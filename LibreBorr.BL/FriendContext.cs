using AutoMapper;
using LibreBorr.BL.Interfaces;
using LibreBorr.BL.Models;
using LibreBorr.Services.Interfaces;

namespace LibreBorr.BL;

public class FriendContext(IFriendService service, IMapper mapper) : IFriendContext
{
    public async Task<List<Friend>> GetFriends()
    {
        var friends = await service.GetFriends();
        return mapper.Map<List<Friend>>(friends);
    }

    public async Task<Friend> GetFriend(int id)
    {
        var friend = await service.GetFriend(id);
        return mapper.Map<Friend>(friend);
    }

    public async Task AddFriend(Friend friend)
    {
        var f = mapper.Map<Services.Models.Friend>(friend);
        await service.AddFriend(f);
    }

    public async Task DeleteFriend(int id)
    {
        await service.DeleteFriend(id);
    }

    public async Task UpdateFriend(Friend friend)
    {
        await service.UpdateFriend(mapper.Map<Services.Models.Friend>(friend));
    }

    public async Task<Book> GetFriendsBooks(Friend friend)
    {
        var books = await service.GetFriendBooks(mapper.Map<Services.Models.Friend>(friend));
        return mapper.Map<Book>(books);
    }
}