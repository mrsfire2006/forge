using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Forge.Api.Features.Shared.Application.Core.CustomMediator;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Forge.Api.Features.Shared
{
    public static class SharedDI
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services)
        {
            var featuresSchema = typeof(FeatureMarker).Assembly;


            AddMediatorServices(services, featuresSchema);
            return services;
        }

        private static void AddMediatorServices(IServiceCollection services, Assembly assembly)
        {
            services.AddTransient<Mediator>();
            var types = assembly.GetTypes();

            foreach (var type in types)
            {
                var handlerInterface = GetHandlerInterface(type);

                if (handlerInterface is null)
                    continue;


                services.AddTransient(handlerInterface, type);

            }
        }
        private static Type? GetHandlerInterface(Type type)
        {
            if (!type.IsClass || type.IsAbstract)
                return null;

            return type
                .GetInterfaces()
                .FirstOrDefault(IsRequestHandler);
        }

        private static bool IsRequestHandler(Type type)
        {
            return type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
        }
    }
}