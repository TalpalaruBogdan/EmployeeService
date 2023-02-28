using EmployeeService.Application.Services.AssignmentService;
using EmployeeService.Application.Services.EmployeeService;
using EmployeeService.Application.Services.ProjectService;
using Microsoft.Extensions.DependencyInjection;
namespace EmployeeService.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, Services.EmployeeService.EmployeeService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IAssignmentService, AssignmentService>();

            return services;
        }
    }
}
