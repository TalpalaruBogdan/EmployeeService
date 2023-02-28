using EmployeeService.Application.Repositories;
using EmployeeService.Domain.Entities;
using EmployeeService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public async Task<Guid> CreateEmployee(Employee employee)
        {
            employee.Assignments = new List<Assignment>();
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee.Id;
        }

        public Task<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees =  _context.Employees.AsEnumerable();
            return Task.FromResult(employees);
        }

        public async Task<Employee?> GetEmployeeByEmail(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByProjectId(Guid projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
            
            var employeeIds = _context.Assignments.Where(x => x.ProjectId == projectId).Select(x => x.EmployeeId);
            
            var employees = employeeIds.Any() 
                ? _context.Employees.Where(x => employeeIds.Contains(x.Id)).AsEnumerable()
                : new List<Employee>();

            return employees;
        }

        public async Task<bool> UpdateEmployee(Guid id, Employee employee)
        {
            var existingEmployee = await GetEmployeeById(id);
            if (existingEmployee == null)
                return false;
            existingEmployee.Email = employee.Email;
            existingEmployee.EndDate = employee.EndDate;
            existingEmployee.Name = employee.Name;
            existingEmployee.Surname = employee.Surname;
            existingEmployee.Email = employee.Email;
            existingEmployee.StartDate = employee.StartDate;
            if (_context.SaveChanges() > 0)
                return true;
            return false;

        }
    }
}
