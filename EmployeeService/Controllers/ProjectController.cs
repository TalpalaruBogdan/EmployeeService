using Azure.Core;
using EmployeeService.Api.Mappings;
using EmployeeService.Application.Services.EmployeeService;
using EmployeeService.Application.Services.ProjectService;
using EmployeeService.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetAllProjects();
            return Ok(projects.ToList().MapToProjectsResponse());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var project = await _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project.MapToProjectResponse());
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProjectByName(string name)
        {
            var project = await _projectService.GetProjectByName(name);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project.MapToProjectResponse());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CreateProject(Guid id, UpdateProjectRequest request)
        {
            var existingProject = await _projectService.GetProjectById(id);
            if (existingProject == null)
            {
                return NotFound();
            }

            var updatedProject = request.MapToProject();
            var success = await _projectService.UpdateProject(id, updatedProject);
            if (success)
            {
                return Ok(updatedProject.MapToProjectResponse());
            }
            else throw new Exception("Something went wrong...");
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectRequest request)
        {
            if (request == null)
            {
                return UnprocessableEntity("Invalid data");
            }
            var employeeSearch = await _projectService.GetProjectByName(request.ProjectName);

            if (employeeSearch is not null)
            {
                return Conflict($"ProjectName {request.ProjectName} already exists");
            }

            var project = request.MapToProject();

            var createdId = await _projectService.CreateProject(project);

            return Created($"api/Project/{createdId}", project.MapToProjectResponse());
        }
    }
}
