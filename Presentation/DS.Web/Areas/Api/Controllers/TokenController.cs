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

namespace DS.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("/api/token")]
    public class TokenController : Controller
    {

        public readonly IAuthService _authService;
        public readonly IUserService _userService;

        public TokenController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
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
                return await DoPassword(parameters, client);
            }
            else
            {
                return BadRequest("Invalid grant_type");
            }

            return Ok(parameters);
        }

        private async Task<IActionResult> DoPassword(TokenAuthParameters parameters, Client client)
        {

            var isValidated = _userService.Queryable().Where(x => x.UserName == parameters.username && x.Password == parameters.password).Count() > 0;

            if (!isValidated)
            {
                return Unauthorized();
            }

            var refreshTokenId = Guid.NewGuid().ToString("p"); // r : Raghav


            var token = new RefreshToken()
            {
                Id = CommonHelper.GetHash(refreshTokenId),
                ClientId = client.Id,
                Subject = parameters.username,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(client.RefreshTokenLifeTime)),
                CreatedBy=0,
                CreatedOn= DateTime.UtcNow,
                IsDeleted=false,
                ModifiedBy=0,
                IsPublished=true,
                ModifiedOn=DateTime.UtcNow,
                ObjectState =  Frameowrk.Repository.Infrastructure.Pattern.ObjectState.Added,
                ProtectedTicket= ""

            };

            

            ////store the refresh_token   
            if (await _authService.AddRefreshToken(token))
            {
                return Ok(GetJwt(parameters.client_id, refreshTokenId));
            }
            else
            {
                return Unauthorized();
            }
        }



        private string GetJwt(string client_id, string refresh_token)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, client_id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var symmetricKeyAsBase64 = "ANY_SECRET_KEY_TO_ENCRYPT";
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var jwt = new JwtSecurityToken(
                issuer: "ISSUER",
                audience: "AUDIENCE",
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)TimeSpan.FromMinutes(2).TotalSeconds,
                refresh_token = refresh_token,
                token_type = "bearer"

            };

            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

    }
}