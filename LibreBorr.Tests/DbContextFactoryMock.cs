using LibreBorr.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreBorr.Tests;

public class DbContextFactoryMock<TContext> : IDbContextFactory<TContext> where TContext : LibreBorrDbContext
{
    private readonly TContext _context;

    public DbContextFactoryMock(TContext context)
    {
        _context = context;
        
    }

    public TContext CreateDbContext()
    {
        return _context;
    }

    public Task<TContext> CreateDbContextAsync()
    {
        return Task.FromResult(_context);
    }
}