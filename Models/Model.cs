
namespace OrigamiHelper.Models;

public class Model
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ComplexityId { get; set; }
    public Complexity? Complexity { get; set; }
    public int SourceId { get; set; }
    public Source? Source { get; set; }
    public int StepCount { get; set; }
    public int UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ModelImg { get; set; }
    public string Artist { get; set; }
    public List<ModelPaper> ModelPapers { get; set; }
}
