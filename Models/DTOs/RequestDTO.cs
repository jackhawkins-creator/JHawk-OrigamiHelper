
namespace OrigamiHelper.Models.DTOs;

public class RequestDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ModelId { get; set; }
    public int StepNumber { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserProfileDTO? UserProfile { get; set; }
    public ModelDTO? Model { get; set; }
}
