﻿using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("AFIExercise.Tests")]

namespace AFIExercise.Services
{
    public static class ServiceExtensions
    {
        public static void AddCustomerRegistrationService(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRegistrationService, CustomerRegistrationService>();
            services.AddSingleton<CustomerRegistrationRequestValidator>();
        }
    }
}
