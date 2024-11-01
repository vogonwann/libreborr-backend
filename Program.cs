using LibreBorr.BL.Extensions;
using LibreBorr.Web.GraphQl.Mutations;
using LibreBorr.Web.Subscriptions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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
    .AddQueryType<BookQuery>()
    .AddMutationType<BookMutation>()
    .AddSubscriptionType<BookSubscription>()
    .AddInMemorySubscriptions();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowLocalhost4200");
app.UseWebSockets();
app.MapGraphQL();

app.Run();
