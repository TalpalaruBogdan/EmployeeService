using EmployeeService.Application.Repositories;
using EmployeeService.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeService.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();

            return services;
        }
    }
}
