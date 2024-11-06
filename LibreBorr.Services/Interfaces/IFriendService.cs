using LibreBorr.Services.Models;

namespace LibreBorr.Services.Interfaces;

public interface IFriendService
{
    Task<List<Friend?>> GetFriends();
    Task<Friend?> GetFriend(int id);
    Task AddFriend(Friend friend);
    Task DeleteFriend(int id);
    Task UpdateFriend(Friend friend);
    Task<List<Book>?> GetFriendBooks(Friend friend);
}