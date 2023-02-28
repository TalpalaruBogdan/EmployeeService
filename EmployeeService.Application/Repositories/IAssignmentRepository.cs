using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Repositories
{
    public interface IAssignmentRepository
    {
        Task<Guid> CreateAssignment(Assignment assignment);

        Task<IEnumerable<Assignment>> GetAssignmentsForEmployee(Guid employeeId);

        Task<IEnumerable<Assignment>> GetAssignmentsForProject(Guid projectId);

        Task<bool> UpdateAssignment(Guid assignmentId, Assignment assignment);

        Task<bool> DeleteAssignment(Guid assignmentId);
    }
}
