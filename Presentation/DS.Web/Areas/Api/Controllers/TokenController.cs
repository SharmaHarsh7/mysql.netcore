using DS.Core;
using DS.Core.Enums;
using DS.Domain.Models.Users;
using DS.Domain.ViewModels;
using DS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using DS.Core.Configuration;

namespace DS.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("/api/token")]
    public class TokenController : Controller
    {

        public readonly IAuthService _authService;
        public readonly IUserService _userService;
        public readonly AuthConfig _authConfig;

        public TokenController(IAuthService authService, IUserService userService, AuthConfig options)
        {
            _authService = authService;
            _userService = userService;
            _authConfig = options;
        }

        [HttpPost("")]
        public async Task<IActionResult> Auth(TokenAuthParameters parameters)
        {
            string clientId = parameters.client_id;
            string clientSecret = parameters.client_secret;
            Client client = null;

            if (clientId == null)
            {
                return BadRequest("invalid_clientId: client_id should be sent.");
            }

            client = _authService.FindClient(clientId);

            if (client == null)
            {
                return BadRequest(string.Format("invalid_clientId: Client '{0}' is not registered in the system.", clientId));
            }

            if (client.ApplicationType == ApplicationType.NativeClient)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    return BadRequest("invalid_clientId: Client secret should be sent.");
                }
                else
                {
                    if (client.Secret != CommonHelper.GetHash(clientSecret))
                    {
                        return BadRequest("invalid_clientId: Client secret is invalid.");

                    }
                }
            }

            if (!client.Active)
            {
                return BadRequest("invalid_clientId: Client is inactive.");
            }

            if (parameters.grant_type == "password")
            {
                return await AuthenticateUser(parameters, client);
            }
            else if (parameters.grant_type == "refresh_token")
            {
                return await RefreshToken(parameters, client);
            }
            else
            {
                return BadRequest("Invalid grant_type");
            }
        }

        private async Task<IActionResult> AuthenticateUser(TokenAuthParameters parameters, Client client)
        {

            var isValidated = _userService.Queryable().Where(x => x.UserName == parameters.username && x.Password == parameters.password).Count() > 0;

            if (!isValidated)
            {
                throw new UnauthorizedAccessException("Invalid user credentials");
            }

            return Ok(await GetJwt(client, parameters));
        }

        private async Task<IActionResult> RefreshToken(TokenAuthParameters parameters, Client client)
        {
            var token = await _authService.FindRefreshToken(CommonHelper.GetHash(parameters.refresh_token));

            if (token == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            // Validate if refresh token expired
            if (token.ExpiresUtc > DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token expired");
            }

            var protectedTicket = new JwtSecurityTokenHandler().ReadJwtToken(token.ProtectedTicket);

            if (await _authService.RemoveRefreshToken(token))
            {
                var userId = protectedTicket.Claims.Where(x => x.Type == "UserId").FirstOrDefault()?.Value ?? "";
                parameters.username = userId;

                return Ok(await GetJwt(client, parameters));
            }

            return Ok("Default OK");
        }



        private async Task<object> GetJwt(Client client, TokenAuthParameters parameters)
        {
            var now = DateTime.UtcNow;

            // Add claim informations
            var claims = new Claim[]
            {
            new Claim("ClientId", client.Id),
            new Claim("UserName", parameters.username),
            new Claim("IssuedAt", now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = _authConfig.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: _authConfig.Issuer,
                audience: _authConfig.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromSeconds(_authConfig.AccessTokenLife)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Generate new refresh token
            var refreshTokenId = Guid.NewGuid().ToString("n"); // r : Raghav

            var token = new RefreshToken()
            {
                Id = CommonHelper.GetHash(refreshTokenId),
                ClientId = client.Id,
                Subject = parameters.username,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(client.RefreshTokenLifeTime)),
                CreatedBy = 0,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
                ModifiedBy = 0,
                IsPublished = true,
                ModifiedOn = DateTime.UtcNow,
                ObjectState = Frameowrk.Repository.Infrastructure.Pattern.ObjectState.Added,
                ProtectedTicket = encodedJwt
            };

            if (await _authService.AddRefreshToken(token))
            {
                var response = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)TimeSpan.FromSeconds(_authConfig.AccessTokenLife).TotalSeconds,
                    refresh_token = refreshTokenId,
                    token_type = "bearer"
                };

                return response;
            }
            else
            {
                return Unauthorized();
            }


        }

    }
}