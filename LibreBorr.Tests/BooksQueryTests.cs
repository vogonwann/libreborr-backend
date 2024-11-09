using HotChocolate;
using HotChocolate.Execution;
using LibreBorr.Web.GraphQl.Mutations;
using LibreBorr.Web.GraphQl.Queries;
using LibreBorr.Web.GraphQl.Subscriptions;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;

namespace LibreBorr.Tests;

public class BooksQueryTests
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        var schema = await new ServiceCollection()
            .AddGraphQL()
            .AddQueryType(d => d.Name("Query"))
                .AddType<BookQuery>()
                .AddType<FriendsQuery>()
            .AddMutationType(d => d.Name("Mutation"))
                .AddType<BookMutation>()
                .AddType<FriendMutation>()
            .AddSubscriptionType<BookSubscription>()
            .AddInMemorySubscriptions()
            .BuildSchemaAsync();
        
        schema.ToString().MatchSnapshot();
    }
    
    [Fact]
    public async Task GetBooksTest()
    {
        var schema = await new ServiceCollection()
            .AddGraphQL()
            .AddQueryType(d => d.Name("Query"))
            .AddType<BookQuery>()
            .AddType<FriendsQuery>()
            .AddMutationType(d => d.Name("Mutation"))
            .AddType<BookMutation>()
            .AddType<FriendMutation>()
            .AddSubscriptionType<BookSubscription>()
            .AddInMemorySubscriptions()
            .ExecuteRequestAsync(@"
                query books {
                  books {
                    id
                    title
                    description
                    tags {
                      name
                    }
                    image
                    authors
                    genres
                  }
                }
                ");
        
        schema.ToJson().MatchSnapshot();
    }
}