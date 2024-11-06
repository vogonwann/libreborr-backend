using LibreBorr.Services.Interfaces;
using LibreBorr.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreBorr.Services;

public class FriendService(IDbContextFactory<LibreBorrDbContext> dbContextFactory) : IFriendService
{
    public async Task<List<Friend?>> GetFriends()
    {
        await using var context = dbContextFactory.CreateDbContext(); 
        {
            return await context.Friends.ToListAsync();
        }
    }

    public async Task<Friend?> GetFriend(int id)
    {
        await using var context = dbContextFactory.CreateDbContext();
        
        return await context.Friends.FirstOrDefaultAsync(f => f.Id == id);
        
    }

    public async Task AddFriend(Friend friend)
    {
        await using var context = dbContextFactory.CreateDbContext();
        
        await context.Friends.AddAsync(friend);
        await context.SaveChangesAsync();
    }

    public async Task DeleteFriend(int id)
    {
        await using var context = dbContextFactory.CreateDbContext();
        
        var friendToDelete = await context.Friends.FirstOrDefaultAsync(f => f.Id == id);
        context.Friends.Remove(friendToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateFriend(Friend friend)
    {
        await using var context = dbContextFactory.CreateDbContext();
        
        context.Friends.Update(friend);
        await context.SaveChangesAsync();
    }

    public async Task<List<Book>?> GetFriendBooks(Friend friend)
    {
        await using var context = dbContextFactory.CreateDbContext();

        var friendWithBooks = await context.Friends.FirstOrDefaultAsync(f => f.Id == friend.Id);
        return friendWithBooks?.Books;
    }
}