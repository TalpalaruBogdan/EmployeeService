using EmployeeService.Application.Repositories;
using EmployeeService.Domain.Entities;
using EmployeeService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateProject(Project project)
        {
            project.Assignments = new List<Assignment>();
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return Guid.NewGuid();
        }

        public Task<IEnumerable<Project>> GetAllProjects()
        {
            var projects = _context.Projects.AsEnumerable();
            return Task.FromResult(projects);
        }

        public async Task<Project?> GetProjectById(Guid projectId)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.Id == projectId);
        }

        public async Task<Project?> GetProjectByName(string projectName)
        {
            return await _context.Projects.FirstOrDefaultAsync(x => x.ProjectName == projectName);
        }

        public async Task<bool> UpdateProject(Guid projectId, Project project)
        {
            var existingProject = await GetProjectById(projectId);
            if (existingProject == null)
                return false;
            existingProject.ProjectName = project.ProjectName;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
