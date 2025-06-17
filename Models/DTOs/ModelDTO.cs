
namespace OrigamiHelper.Models.DTOs;

public class ModelDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ComplexityId { get; set; }
    public ComplexityDTO Complexity { get; set; }
    public int PaperId { get; set; }
    public PaperDTO Paper { get; set; }
    public int SourceId { get; set; }
    public SourceDTO Source { get; set; }
    public int StepCount { get; set; }
    public int UserProfileId { get; set; }
    public UserProfileDTO UserProfile { get; set; }
    public DateTime CreatedAt { get; set; }
    public string ModelImg { get; set; }
    public List<ModelPaperDTO> ModelPapers { get; set; }
}
