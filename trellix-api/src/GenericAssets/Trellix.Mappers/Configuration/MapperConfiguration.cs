namespace Trellix.Mappers.Configuration;

using Microsoft.Extensions.DependencyInjection;
using Trellix.Mappers.Profiles.User;

public static class MappersConfiguration
{
    public static void AddMapConfiguration(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(AttachmentProfile).Assembly);
    }
}