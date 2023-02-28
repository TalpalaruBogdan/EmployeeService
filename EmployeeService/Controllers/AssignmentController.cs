using EmployeeService.Api.Mappings;
using EmployeeService.Application.Services.AssignmentService;
using EmployeeService.Application.Services.EmployeeService;
using EmployeeService.Application.Services.ProjectService;
using EmployeeService.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace EmployeeService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IProjectService _projectService;

        public AssignmentController(IAssignmentService assignmentService, IEmployeeService employeeService, IProjectService projectService)
        {
            _assignmentService = assignmentService;
            _employeeService = employeeService;
            _projectService = projectService;
        }

        [HttpGet("byProjectId/{projectId}")]
        public async Task<IActionResult> GetAssignmentsByProjectId(Guid projectId)
        {
            var results = await _assignmentService.GetAssignmentsForProject(projectId);
            return Ok(results.ToList().MapToAssignmentsResponse());
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> DeleteAssignmentById(Guid id)
        {
            var results = await _assignmentService.DeleteAssignment(id);
            return results == true ?
                NoContent() :
                NotFound(id);
        }

        [HttpGet("byEmployeeId/{employeeId}")]
        public async Task<IActionResult> GetAssignmentsByEmployeeId(Guid employeeId)
        {
            var results = await _assignmentService.GetAssignmentsForEmployee(employeeId);
            return Ok(results.ToList().MapToAssignmentsResponse());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssignment(CreateAssignmentRequest request)
        {
            if (request == null)
            {
                return UnprocessableEntity("Invalid data");
            }
            var employeeSearch = await _assignmentService.GetAssignmentsForEmployee(request.EmployeeId);
            var projectSearch = await _assignmentService.GetAssignmentsForProject(request.ProjectId);

            if (employeeSearch.Any(x => x.ProjectId == request.ProjectId))
            {
                return Conflict($"Assignment already exists for employee under project");
            }

            var assignment = request.MapToAssignment();

            var employee = await _employeeService.GetEmployeeById(request.EmployeeId);
            var project = await _projectService.GetProjectById(request.ProjectId);

            assignment.Employee = employee;
            assignment.Project = project;

            var createdId = await _assignmentService.CreateAssignment(assignment);

            return Created($"api/Assignment/{createdId}", assignment.MapToAssignmentResponse());
        }
    }
}
