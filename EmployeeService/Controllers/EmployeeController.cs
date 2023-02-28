using EmployeeService.Api.Mappings;
using EmployeeService.Application.Services.EmployeeService;
using EmployeeService.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();
            return Ok(employees.ToList().MapToEmployeesResponse());
        }

        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee.MapToEmployeeResponse());
        }

        [HttpGet("byEmail/{email}")]
        public async Task<IActionResult> GetEmployeeByEmail(string email)
        {
            var employee = await _employeeService.GetEmployeeByEmail(email);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee.MapToEmployeeResponse());
        }
        
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetEmployeeByProjectId(Guid projectId)
        {
            var employees = await _employeeService.GetEmployeesByProjectId(projectId);
            return Ok(employees.ToList().MapToEmployeesResponse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeRequest request)
        {
            var existingEmployee = await _employeeService.GetEmployeeById(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            var updatedEmployee = request.MapToEmployee();
            var success = await _employeeService.UpdateEmployee(id, updatedEmployee!);
            if (success)
            {
                return Ok(updatedEmployee.MapToEmployeeResponse());
            }
            else throw new Exception("Something went wrong...");
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest request)
        {
            if (request == null)
            {
                return UnprocessableEntity("Invalid data");
            }
            var employeeSearch = await _employeeService.GetEmployeeByEmail(request.Email);

            if (employeeSearch is not null)
            {
                return Conflict($"Email {request.Email} already exists");
            }

            var employee = request.MapToEmployee();

            var createdId = await _employeeService.CreateEmployee(employee);

            return Created($"api/Employee/{createdId}", employee.MapToEmployeeResponse());
            
        }
    }
}
