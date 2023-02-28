using EmployeeService.Application.Repositories;
using EmployeeService.Domain.Entities;
using EmployeeService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AssignmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> CreateAssignment(Assignment assignment)
        {

            var referencedEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == assignment.EmployeeId);
            var referencedProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == assignment.ProjectId);

            if (referencedEmployee != null && referencedProject != null)
            {
                assignment.Employee = referencedEmployee;
                assignment.Project = referencedProject;

                if (referencedEmployee.Assignments is null)
                {
                    referencedEmployee.Assignments = new List<Assignment>();
                }

                if (referencedProject.Assignments is null)
                {
                    referencedProject.Assignments = new List<Assignment>();
                }

                if (referencedEmployee.Assignments.Any(x => x.Id == assignment.Id))
                {
                    var existingEmployeeAssignment = referencedEmployee.Assignments.FirstOrDefault(x => x.Id == assignment.Id);
                    existingEmployeeAssignment = assignment;
                }
                else
                {
                    referencedEmployee.Assignments.Add(assignment);
                }

                if (referencedProject.Assignments.Any(x => x.Id == assignment.Id))
                {
                    var existingProjectAssignment = referencedProject.Assignments.FirstOrDefault(x => x.Id == assignment.Id);
                    existingProjectAssignment = assignment;
                }
                else
                {
                    referencedEmployee.Assignments.Add(assignment);
                }

            }
            else throw new Exception("Error finding Employee or project");

            await _context.Assignments.AddAsync(assignment);

            await _context.SaveChangesAsync();
            return assignment.Id;
        }

        public async Task<bool> DeleteAssignment(Guid assignmentId)
        {
            var assignment = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == assignmentId);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsForEmployee(Guid employeeId)
        {
            return await _context.Assignments.Where(x => x.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsForProject(Guid projectId)
        {
            return await _context.Assignments.Where(x => x.ProjectId == projectId).ToListAsync();
        }

        public async Task<bool> UpdateAssignment(Guid assignmentId, Assignment assignment)
        {
            var existingAssignment = await _context.Assignments.FirstOrDefaultAsync(x => x.Id == assignmentId);
            if (existingAssignment == null)
                return false;
            existingAssignment.EndDate = assignment.EndDate;
            existingAssignment.StartDate = assignment.StartDate;
            existingAssignment.Role = assignment.Role;
            existingAssignment.ProjectId = assignment.ProjectId;
            existingAssignment.EmployeeId = assignment.EmployeeId;

            var referencedEmployee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == assignment.EmployeeId);
            var referencedProject = await _context.Projects.FirstOrDefaultAsync(x => x.Id == assignment.ProjectId);

            if (referencedEmployee != null && referencedProject != null)
            {
                if (referencedEmployee.Assignments.Any(x => x.Id == assignmentId)) 
                {
                    var existingEmployeeAssignment = referencedEmployee.Assignments.FirstOrDefault(x => x.Id == assignmentId);
                    existingEmployeeAssignment = assignment;
                }
                else
                {
                    referencedEmployee.Assignments.Add(assignment);
                }

                if (referencedProject.Assignments.Any(x => x.Id == assignmentId))
                {
                    var existingProjectAssignment = referencedProject.Assignments.FirstOrDefault(x => x.Id == assignmentId);
                    existingProjectAssignment = assignment;
                }
                else
                {
                    referencedEmployee.Assignments.Add(assignment);
                }

                existingAssignment.Employee = referencedEmployee;
                existingAssignment.Project = referencedProject;
            }
            else throw new Exception("Error finding Employee or project");

            if (_context.SaveChanges() > 0)
                return true;
            return false;
        }
    }
}
