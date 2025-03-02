namespace Trellix.Mappers;

using Microsoft.Extensions.DependencyInjection;
using Trellix.Mappers.Profiles.User;

public static class MappersConfiguration
{
    public static void AddMapConfiguration(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(AttachmentProfile).Assembly);
    }
}