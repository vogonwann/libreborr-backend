using LibreBorr.FetchBookApi.Refit;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();  // Ako koristiš kolačiće ili autentifikaciju
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services
    .AddRefitClient<IGoogleBooksApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.googleapis.com/books/v1"));
builder.Services.AddRefitClient<IGoogleBooksImageApi>()
    .ConfigureHttpClient(c => 
    {
        c.BaseAddress = new Uri("http://books.google.com");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowLocalhost4200");
app.MapControllers();

app.Run();


