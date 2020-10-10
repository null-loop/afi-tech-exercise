﻿using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AFIExercise.Data
{
    public static class ServiceExtensions
    {
        public static void AddSqlServerDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, SqlServerUnitOfWork>();
            services.AddTransient<ICustomerRegistrationRepository, SqlServerCustomerRegistrationRepository>();
            services.AddDbContext<SqlServerDbContext>(options => options.UseSqlServer(configuration["SqlServer:ConnectionString"]));
        }
    }
}