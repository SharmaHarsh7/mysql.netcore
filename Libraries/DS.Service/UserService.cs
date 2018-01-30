using DS.Domain.Models.Users;
using DS.Frameowrk.Repository.Repositories.Pattern;
using DS.Frameowrk.Repository.UnitOfWork.Pattern;
using DS.Frameowrk.Service.Pattern;
using System.Linq;
using System.Threading.Tasks;

namespace DS.Services
{
    public interface IUserService : IService<User>
    {
        
        User GetUser(int id);
        new Task<User> Insert(User user);
        new User Update(User user);
        void Delete(int userId);

    }

    public class UserService: Service<User> , IUserService
    {
        public int currentUser { get; set; }
        private readonly IRepositoryAsync<User> _userRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public UserService(IRepositoryAsync<User> userRepository, IUnitOfWorkAsync unitOfWorkAsync) : base (userRepository)
        {
            _userRepository = userRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public User GetUser(int id)
        {
            return _userRepository.Query().Select().Where(u => u.ID_User == id).FirstOrDefault();
        }


        public new async Task<User> Insert(User user)
        {
            _userRepository.Insert(user);

           await _unitOfWorkAsync.SaveChangesAsync();

            return user;
        }

        public new User Update( User user)
        {
            _userRepository.Update(user);
            _unitOfWorkAsync.SaveChanges();

            return user;
        }

        public void Delete(int userId)
        {
            _userRepository.Delete(GetUser(userId));
        }



    }
}
