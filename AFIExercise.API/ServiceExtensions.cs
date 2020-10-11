using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AFIExercise.API
{
    public static class ServiceExtensions
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddSingleton(CreateMapper());
        }

        internal static Mapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.CustomerRegistrationRequest, Services.CustomerRegistrationRequest>();
                cfg.CreateMap<Services.ValidationMessage, Models.ValidationMessage>();
            });

            var mapper = new Mapper(mapperConfiguration);
            return mapper;
        }
    }
}
