using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<Guid> CreateEmployee(Employee employee);

        public Task<bool> UpdateEmployee(Guid id, Employee employee);

        public Task<IEnumerable<Employee>> GetAllEmployees();

        public Task<IEnumerable<Employee>> GetEmployeesByProjectId(Guid projectId);

        public Task<Employee?> GetEmployeeById(Guid id);

        public Task<Employee?> GetEmployeeByEmail(string email);
    }
}
