using LibreBorr.Web.GraphQl.Mutations;
using LibreBorr.Web.Responses;

namespace LibreBorr.Web.GraphQl.Subscriptions;

public class BookSubscription
{
    [Subscribe]
    [Topic(nameof(BookMutation.CreateBook))]
    public BookResponse OnBookAdded([EventMessage]BookResponse book) => book;
    [Subscribe]
    [Topic(nameof(BookMutation.UpdateBook))]
    public BookResponse BookUpdated([EventMessage]BookResponse book) => book;
    [Subscribe]
    [Topic(nameof(BookMutation.DeleteBook))]
    public BookResponse OnBookDeleted([EventMessage]BookResponse book) => book;
}