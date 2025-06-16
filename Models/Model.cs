
namespace OrigamiHelper.Models;

public class Model
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ComplexityId { get; set; }
    public int PaperId { get; set; }
    public int SourceId { get; set; }
    public int StepCount { get; set; }
    public int CreatorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ModelImg { get; set; }
    public string Artist { get; set; }
}
