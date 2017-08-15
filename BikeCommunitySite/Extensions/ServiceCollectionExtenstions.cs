using BikeCommunitySite.Services;
using BikeSite.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BikeCommunitySite.Extentions
{
    public static class ServiceCollectionExtenstions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IPlaceService, PlaceService>();

            return services;
        }
    }
}
