using Microsoft.Extensions.DependencyInjection;
using ZaloPayGateway.Services;

namespace ZaloPayGateway
{
    public static class ZaloPayGatewayConfigServiceCollectionExtensions
    {
        public static void AddZaloPayGateway(this IServiceCollection services)
        {
            services.AddScoped<IZaloPayServices,ZaloPayServices>();
        }
    }
}