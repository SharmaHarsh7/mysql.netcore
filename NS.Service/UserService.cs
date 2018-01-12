using DS.Frameowrk.Service.Pattern;
using NS.Data.Models;
using NS.Domain.Models.Users;
using NS.Frameowrk.Repository.Repositories.Pattern;
using NS.Frameowrk.Repository.UnitOfWork.Pattern;
using NS.Frameowrk.Service.Pattern;
using System.Linq;

namespace NS.Services
{
    public interface IUserService : IService<User>
    {
        
        User GetUser(int id);
        new User Insert(User user);
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


        public new User Insert(User user)
        {
            _userRepository.Insert(user);

           _unitOfWorkAsync.SaveChanges();

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
