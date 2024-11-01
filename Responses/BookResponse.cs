using System;

namespace LibreBorr.Web.Responses;

public class BookResponse : ResponseBase
{
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string Action {get; set;}

    public string Message
    {
        get => $"Mutation {Action} for book {Title} executed successfully!";
    }

    public BookResponse(string title, string action, int? id = null, string? description = null)
    {
        Title = title;
        Id = id;
        Description = description;
    }
}
