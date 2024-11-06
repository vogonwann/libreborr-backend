using AutoMapper;
using LibreBorr.BL.GraphQl.Inputs;
using LibreBorr.BL.Interfaces;
using LibreBorr.BL.Models;

namespace LibreBorr.Web.GraphQl.Mutations;

[ExtendObjectType(Name = "Mutation")]
public class FriendMutation(IFriendContext context, ILogger<FriendMutation> logger, IMapper mapper)
{
    public async Task AddFriend(FriendInput friendInput)
    {
        var friend = mapper.Map<Friend>(friendInput);
        await context.AddFriend(friend);
    }
}