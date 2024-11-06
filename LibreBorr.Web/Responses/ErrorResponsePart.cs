namespace LibreBorr.Web.Responses;

public class ErrorResponsePart
{
    public string? InnerMessage { get; set; }
    public string? Message { get; set; }
    public int Code { get; set; }
}
