using EmployeeService.Application.Repositories;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<Guid> CreateProject(Project project)
        {
            return await _projectRepository.CreateProject(project);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _projectRepository.GetAllProjects();
        }

        public async Task<Project?> GetProjectById(Guid projectId)
        {
            return await _projectRepository.GetProjectById(projectId);
        }

        public async Task<Project?> GetProjectByName(string projectName)
        {
            return await _projectRepository.GetProjectByName(projectName);
        }

        public async Task<bool> UpdateProject(Guid projectId, Project project)
        {
            return await _projectRepository.UpdateProject(projectId, project);
        }
    }
}
