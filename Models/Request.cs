namespace OrigamiHelper.Models;

public class Request
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ModelId { get; set; }
    public int StepNumber { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public UserProfile? UserProfile { get; set; }
    public Model? Model { get; set; }
}
