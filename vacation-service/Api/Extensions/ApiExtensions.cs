using Api.Services;
using Api.Services.Common;
using Api.Services.Interfaces;
using IDepartmentRightService = Api.Services.Common.Interfaces.IDepartmentRightService;

namespace Api.Extensions;

public static class ApiExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IRequestService, RequestService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IDepartmentRightService, DepartmentRightService>();
        return services;
    }
}