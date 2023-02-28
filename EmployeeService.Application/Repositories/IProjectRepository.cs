using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Repositories
{
    public interface IProjectRepository
    {
        Task<Guid> CreateProject(Project project);

        Task<bool> UpdateProject(Guid projectId, Project project);

        Task<IEnumerable<Project>> GetAllProjects();

        Task<Project?> GetProjectById(Guid projectId);

        Task<Project?> GetProjectByName(string projectName);
    }
}
