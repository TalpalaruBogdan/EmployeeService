using EmployeeService.Application.Repositories;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Application.Services.AssignmentService
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public AssignmentService(IAssignmentRepository assignmentRepository) 
        {
            _assignmentRepository = assignmentRepository;
        }

        public async Task<Guid> CreateAssignment(Assignment assignment)
        {
            return await _assignmentRepository.CreateAssignment(assignment);
        }

        public async Task<bool> DeleteAssignment(Guid assignmentId)
        {
            return await _assignmentRepository.DeleteAssignment(assignmentId);        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsForEmployee(Guid employeeId)
        {
            return await _assignmentRepository.GetAssignmentsForEmployee(employeeId);
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsForProject(Guid projectId)
        {
            return await _assignmentRepository.GetAssignmentsForProject(projectId);
        }

        public async Task<bool> UpdateAssignment(Guid assignmentId, Assignment assignment)
        {
            return await _assignmentRepository.UpdateAssignment(assignmentId, assignment);
        }
    }
}
