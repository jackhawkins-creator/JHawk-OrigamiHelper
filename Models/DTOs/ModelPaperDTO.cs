
namespace OrigamiHelper.Models.DTOs;

public class ModelPaperDTO
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public int PaperId { get; set; }
    public PaperDTO? Paper { get; set; }
}
