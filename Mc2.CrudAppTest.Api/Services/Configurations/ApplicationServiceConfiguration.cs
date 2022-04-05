
using Mc2.CrudAppTest.Api.Services.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Mc2.CrudAppTest.Api.Services.Configurations
{
    public static class ApplicationServiceConfiguration
    {
        private static IServiceCollection AddMediatorHandlers(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
                {
                    services.AddTransient(handlerType.AsType(), type.AsType());
                }


            }

            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {


            services.AddMediatR(typeof(CreateCustomerCommandHandler));

            services.AddMediatorHandlers(typeof(CreateCustomerCommandHandler).Assembly);

            return services;
        }
    }
}
