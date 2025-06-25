
namespace OrigamiHelper.Models.DTOs;

public class ResponseDTO
{
    public int Id { get; set; }
    public int RequestId { get; set; }
    public int ResponderId { get; set; }
    public string Media { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public RequestDTO Request { get; set; }
}
