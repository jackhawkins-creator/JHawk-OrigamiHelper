
namespace OrigamiHelper.Models;

public class ModelPaper
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public int PaperId { get; set; }
    public Paper? Paper { get; set; }
}
