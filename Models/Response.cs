namespace OrigamiHelper.Models;

public class Response
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public int ResponderId { get; set; }
    public string Media { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public Request? Request { get; set; }
}
