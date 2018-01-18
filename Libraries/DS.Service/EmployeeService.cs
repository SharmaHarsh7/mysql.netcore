using DS.Domain.Models.Users;
using DS.Frameowrk.Repository.Repositories.Pattern;
using DS.Frameowrk.Repository.UnitOfWork.Pattern;
using DS.Frameowrk.Service.Pattern;
using System.Linq;

namespace DS.Services
{
    public interface IEmployeeService : IService<Employee>
    {
        
        Employee GetEmployee(int id);
        new Employee Insert(Employee employee);
        new Employee Update(Employee employee);
        void Delete(int employeeId);

    }

    public class EmployeeService: Service<Employee> , IEmployeeService
    {
        public int currentEmployee { get; set; }
        private readonly IRepositoryAsync<Employee> _employeeRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public EmployeeService(IRepositoryAsync<Employee> employeeRepository, IUnitOfWorkAsync unitOfWorkAsync) : base (employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeRepository.Query().Select().Where(u => u.ID_Employee == id).FirstOrDefault();
        }

        public new Employee Insert(Employee employee)
        {
            _employeeRepository.Insert(employee);

           _unitOfWorkAsync.SaveChanges();

            return employee;
        }

        public new Employee Update( Employee employee)
        {
            _employeeRepository.Update(employee);
            _unitOfWorkAsync.SaveChanges();

            return employee;
        }

        public void Delete(int employeeId)
        {
            _employeeRepository.Delete(GetEmployee(employeeId));
        }



    }
}
