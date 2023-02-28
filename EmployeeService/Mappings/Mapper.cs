using Azure;
using EmployeeService.Contracts.Requests;
using EmployeeService.Contracts.Responses;
using EmployeeService.Domain.Entities;

namespace EmployeeService.Api.Mappings
{
    public static class Mapper
    {

        #region employee

        public static EmployeeResponse MapToEmployeeResponse(this Employee employee)
        {
            var response = new EmployeeResponse();
            response.Id = employee.Id;
            response.StartDate = employee.StartDate;
            response.EndDate = employee.EndDate;
            response.Name = employee.Name;
            response.Surname = employee.Surname;
            response.Email = employee.Email;
            return response;
        }

        public static EmployeesResponse MapToEmployeesResponse(this List<Employee> employees)
        {
            var response = new EmployeesResponse();
            response.Items = employees.Select(x => x.MapToEmployeeResponse());
            return response;
        }

        public static Employee MapToEmployee(this EmployeeResponse response)
        {
            var employee = new Employee();
            employee.Id = response.Id;
            employee.StartDate = response.StartDate;
            employee.EndDate = response.EndDate;
            employee.Name = response.Name;
            employee.Surname = response.Surname;
            employee.Email = response.Email;
            return employee;
        }

        public static Employee MapToEmployee(this CreateEmployeeRequest request)
        {
            var employee = new Employee();
            employee.Id = Guid.NewGuid();
            employee.StartDate = request.StartDate;
            employee.EndDate = request.EndDate;
            employee.Name = request.Name;
            employee.Surname = request.Surname;
            employee.Email = request.Email;
            return employee;
        }

        public static Employee MapToEmployee(this UpdateEmployeeRequest request)
        {
            var employee = new Employee();
            employee.StartDate = request.StartDate;
            employee.EndDate = request.EndDate;
            employee.Name = request.Name;
            employee.Surname = request.Surname;
            employee.Email = request.Email;
            return employee;
        }

        #endregion


        #region project

        public static ProjectResponse MapToProjectResponse(this Project project)
        {
            var response = new ProjectResponse();
            response.Id = project.Id;
            response.StartDate = project.StartDate;
            response.EndDate = project.EndDate;
            response.ProjectName = project.ProjectName;
            return response;
        }

        public static ProjectsResponse MapToProjectsResponse(this List<Project> projects)
        {
            var response = new ProjectsResponse();
            response.Items = projects.Select(x => x.MapToProjectResponse());
            return response;
        }

        public static Project MapToProject(this ProjectResponse response)
        {
            var project = new Project();
            project.Id = response.Id;
            project.StartDate = response.StartDate;
            project.EndDate = response.EndDate;
            project.ProjectName = response.ProjectName;
            return project;
        }

        public static Project MapToProject(this CreateProjectRequest request)
        {
            var project = new Project();
            project.Id = Guid.NewGuid();
            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;
            project.ProjectName = request.ProjectName;
            return project;
        }

        public static Project MapToProject(this UpdateProjectRequest request)
        {
            var project = new Project();
            project.StartDate = request.StartDate;
            project.EndDate = request.EndDate;
            project.ProjectName = request.ProjectName;
            return project;
        }

        #endregion


        #region assignment

        public static AssignmentResponse MapToAssignmentResponse(this Assignment assignment)
        {
            var response = new AssignmentResponse();
            response.Id = assignment.Id;
            response.StartDate = assignment.StartDate;
            response.EndDate = assignment.EndDate;
            response.EmployeeId = assignment.EmployeeId;
            response.ProjectId= assignment.ProjectId;
            response.Role = assignment.Role;
            return response;
        }

        public static AssignmentsResponse MapToAssignmentsResponse(this List<Assignment> assignments)
        {
            var response = new AssignmentsResponse();
            response.Items = assignments.Select(x => x.MapToAssignmentResponse());
            return response;
        }

        public static Assignment MapToAssignment(this AssignmentResponse response)
        {
            var assignment = new Assignment();
            assignment.Id = response.Id;
            assignment.StartDate = response.StartDate;
            assignment.EndDate = response.EndDate;
            assignment.EmployeeId = response.EmployeeId;
            assignment.ProjectId = response.ProjectId;
            assignment.Role = response.Role;
            return assignment;
        }

        public static Assignment MapToAssignment(this CreateAssignmentRequest request)
        {
            var assignment = new Assignment();
            assignment.Id = Guid.NewGuid();
            assignment.StartDate = request.StartDate;
            assignment.EndDate = request.EndDate;
            assignment.EmployeeId = request.EmployeeId;
            assignment.ProjectId = request.ProjectId;
            assignment.Role = request.Role;
            return assignment;
        }

        public static Assignment MapToAssignment(this UpdateAssignmentRequest request)
        {
            var assignment = new Assignment();
            assignment.StartDate = request.StartDate;
            assignment.EndDate = request.EndDate;
            assignment.EmployeeId = request.EmployeeId;
            assignment.ProjectId = request.ProjectId;
            assignment.Role = request.Role;
            return assignment;
        }

        #endregion
    }
}
