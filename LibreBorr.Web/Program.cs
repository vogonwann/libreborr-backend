using LibreBorr.BL.Extensions;
using LibreBorr.Services.Extensions;
using LibreBorr.Services.Models;
using LibreBorr.Web.GraphQl.Mutations;
using LibreBorr.Web.GraphQl.Queries;
using LibreBorr.Web.Subscriptions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.

var connectionString = builder.Configuration["DB_CONNECTION_STRING"];;
builder.Services.AddServiceLayer(connectionString);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddBLLayer();
builder.Services.AddHttpClient();

builder.Services.AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
        .AddType<BookQuery>()
        .AddType<FriendsQuery>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddType<BookMutation>()
        .AddType<FriendMutation>()
    .AddSubscriptionType<BookSubscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowLocalhost4200");
app.UseWebSockets();
app.MapGraphQL();

app.Run();
