using DS.Code.Domain.Models.Authentication;
using DS.Domain.Models.Users;
using DS.Frameowrk.Repository.Repositories.Pattern;
using DS.Frameowrk.Repository.UnitOfWork.Pattern;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Services
{

    public interface IAuthService
    {
        Client FindClient(string clientId);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();
    }

    public class AuthService : IAuthService
    {
        private readonly IRepositoryAsync<Client> _clientRepository;
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IRepositoryAsync<RefreshToken> _refreshTokenRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public AuthService(IRepositoryAsync<Client> clientRepository, IRepositoryAsync<RefreshToken> refreshTokenRepository, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _clientRepository = clientRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public Client FindClient(string clientId)
        {
            var client = _clientRepository.Queryable().Where(x => x.Id == clientId).FirstOrDefault();

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _refreshTokenRepository.Queryable().Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _refreshTokenRepository.Insert(token);

            return await _unitOfWorkAsync.SaveChangesAsync(false) > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _refreshTokenRepository.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _refreshTokenRepository.Delete(refreshToken);
                return await _unitOfWorkAsync.SaveChangesAsync(true) > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokenRepository.Delete(refreshToken);
            return await _unitOfWorkAsync.SaveChangesAsync(true) > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _refreshTokenRepository.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _refreshTokenRepository.Queryable().ToList();
        }

        
    }
}
