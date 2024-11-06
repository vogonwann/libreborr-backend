using LibreBorr.Services.Interfaces;
using LibreBorr.Services.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibreBorr.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection serviceCollection, string connectionString) 
    {
        serviceCollection.AddDbContextFactory<LibreBorrDbContext>(options =>{
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("LibreBorr.Web"));
        });

        serviceCollection.AddScoped<IBookService, BookService>();
        serviceCollection.AddScoped<IFriendService, FriendService>();

        return serviceCollection;
    }
}