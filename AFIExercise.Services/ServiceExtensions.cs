using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("AFIExercise.Tests")]

namespace AFIExercise.Services
{
    public static class ServiceExtensions
    {
        public static void AddRegistrationService(this IServiceCollection services)
        {
            services.AddTransient<IRegistrationService, RegistrationService>();
        }
    }
}
