using EmployeeService.Application.Repositories;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> CreateEmployee(Employee employee)
        {
            return await _employeeRepository.CreateEmployee(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepository.GetAllEmployees();
        }

        public async Task<Employee?> GetEmployeeByEmail(string email)
        {
            return await _employeeRepository.GetEmployeeByEmail(email);
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByProjectId(Guid projectId)
        {
            return await _employeeRepository.GetEmployeesByProjectId(projectId);
        }

        public async Task<bool> UpdateEmployee(Guid id, Employee employee)
        {
            return await _employeeRepository.UpdateEmployee(id, employee);
        }
    }
}
