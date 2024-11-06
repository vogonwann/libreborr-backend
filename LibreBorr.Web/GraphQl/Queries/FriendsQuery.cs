using LibreBorr.BL.Interfaces;
using LibreBorr.BL.Models;

namespace LibreBorr.Web.GraphQl.Queries;

[ExtendObjectType(Name = "Query")]
public class FriendsQuery(IFriendContext friendContext, ILogger<FriendsQuery> logger)
{
    public async Task<List<Friend>> GetFriends()
    {
        return await friendContext.GetFriends();
    }
        
}