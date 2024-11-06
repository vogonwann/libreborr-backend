using LibreBorr.BL.Models;

namespace LibreBorr.BL.Interfaces;

public interface IFriendContext
{
    Task<List<Friend>> GetFriends();
    Task<Friend> GetFriend(int id);
    Task AddFriend(Friend friend);
    Task DeleteFriend(int id);
    Task UpdateFriend(Friend friend);
    Task<Book> GetFriendsBooks(Friend friend);
}