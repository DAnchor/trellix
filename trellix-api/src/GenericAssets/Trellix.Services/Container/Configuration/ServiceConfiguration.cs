namespace Trellix.Services.Container.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Trellix.Repositories.Crud;
using Trellix.DataAccess.Repositories.Crud;

public static class ServiceConfiguration
{
    public static void AddServicesConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAttachmentService, AttachmentService>();
    }
}