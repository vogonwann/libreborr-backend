using LibreBorr.BL.Interfaces;
using LibreBorr.BL.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace LibreBorr.BL.Extensions;

public static class BLServiceCollectionExtensions
{
    public static IServiceCollection AddBLLayer(this IServiceCollection services) 
    {
        services.AddAutoMapper(typeof(LibreBorrAutoMapperProfile));
        services.AddScoped<IBooksContext, BooksContext>();
        services.AddScoped<IFriendContext, FriendContext>();
        return services;
    }
}