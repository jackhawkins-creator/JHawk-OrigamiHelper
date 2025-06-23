using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrigamiHelper.Data;
using Microsoft.EntityFrameworkCore;
using OrigamiHelper.Models;
using OrigamiHelper.Models.DTOs;

namespace OrigamiHelper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModelController : ControllerBase
{
    private OrigamiHelperDbContext _dbContext;

    public ModelController(OrigamiHelperDbContext context)
    {
        _dbContext = context;
    }

    // Helper: Map Model → ModelDTO
    private ModelDTO MapToDTO(Model model)
    {
        return new ModelDTO
        {
            Id = model.Id,
            Title = model.Title,
            ComplexityId = model.ComplexityId,
            Complexity = model.Complexity == null ? null : new ComplexityDTO
            {
                Id = model.Complexity.Id,
                Difficulty = model.Complexity.Difficulty
            },
            SourceId = model.SourceId,
            Source = model.Source == null ? null : new SourceDTO
            {
                Id = model.Source.Id,
                Title = model.Source.Title
            },
            StepCount = model.StepCount,
            UserProfileId = model.UserProfileId,
            UserProfile = model.UserProfile == null ? null : new UserProfileDTO
            {
                Id = model.UserProfile.Id,
                FirstName = model.UserProfile.FirstName,
                LastName = model.UserProfile.LastName,
                Address = model.UserProfile.Address,
                IdentityUserId = model.UserProfile.IdentityUserId
            },
            CreatedAt = model.CreatedAt,
            ModelImg = model.ModelImg,
            Artist = model.Artist,
            ModelPapers = model.ModelPapers?.Select(mp => new ModelPaperDTO
            {
                Id = mp.Id,
                ModelId = mp.ModelId,
                PaperId = mp.PaperId,
                Paper = mp.Paper == null ? null : new PaperDTO
                {
                    Id = mp.Paper.Id,
                    Brand = mp.Paper.Brand
                }
            }).ToList() ?? new List<ModelPaperDTO>()
        };
    }

    //GET all models, newest to oldest
    [HttpGet]
    //[Authorize]
    public IActionResult GetAllModels()
    {
        List<Model> models = _dbContext.Models
            .Include(m => m.UserProfile)
            .Include(m => m.Complexity)
            .Include(m => m.Source)
            .Include(m => m.ModelPapers)
                .ThenInclude(mp => mp.Paper)
            .OrderByDescending(m => m.CreatedAt)
            .ToList();

        List<ModelDTO> modelDTOs = models.Select(MapToDTO).ToList();
        return Ok(modelDTOs);
    }

    //GET single Model by id
    [HttpGet("{id}")]
    //[Authorize]
    public IActionResult GetById(int id)
    {
        Model model = _dbContext.Models
            .Include(m => m.UserProfile)
            .Include(m => m.Complexity)
            .Include(m => m.Source)
            .Include(m => m.ModelPapers)
                .ThenInclude(mp => mp.Paper)
            .SingleOrDefault(m => m.Id == id);

        if (model == null)
        {
            return NotFound();
        }

        return Ok(MapToDTO(model));
    }

    //POST Model
    //Raw Body Test Data:
    /*
    {
    "title": "Flapping Bird",
    "complexityId": 3,
    "sourceId": 1,
    "stepCount": 45,
    "userProfileId": 1,
    "modelImg": "flapping_bird.png",
    "artist": "Unknown",
    "modelPapers": [
        { "paperId": 1 },
        { "paperId": 2 }
    ]
    }
    */
    [HttpPost]
    //[Authorize]
    public IActionResult CreateModel([FromBody] ModelDTO modelDTO)
    {
        if (modelDTO == null)
        {
            return BadRequest("ModelDTO is required");
        }

        Model model = new Model
        {
            Title = modelDTO.Title,
            ComplexityId = modelDTO.ComplexityId,
            SourceId = modelDTO.SourceId,
            StepCount = modelDTO.StepCount,
            UserProfileId = modelDTO.UserProfileId,
            ModelImg = modelDTO.ModelImg,
            Artist = modelDTO.Artist,
            CreatedAt = DateTime.UtcNow,
            ModelPapers = modelDTO.ModelPapers?.Select(mp => new ModelPaper
            {
                PaperId = mp.PaperId
            }).ToList() ?? new List<ModelPaper>()
        };

        _dbContext.Models.Add(model);
        _dbContext.SaveChanges();

        return Created($"/api/model/{model.Id}", MapToDTO(model));
    }

    //DELETE Model
    [HttpDelete("{id}")]
    // [Authorize]
    public IActionResult DeleteModel(int id)
    {
        Model model = _dbContext.Models.SingleOrDefault(m => m.Id == id);
        if (model == null)
        {
            return NotFound();
        }

        string currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        UserProfile userProfile = _dbContext.UserProfiles.SingleOrDefault(up => up.IdentityUserId == currentUserId);

        if (userProfile == null || userProfile.Id != model.UserProfileId)
        {
            return Forbid("You can only delete your own models.");
        }

        _dbContext.Models.Remove(model);
        _dbContext.SaveChanges();

        return NoContent();
    }


    //PUT Model
    //Raw Body Test Data:
    /*
    {
    "id": 1,
    "title": "Flapping Bird Updated",
    "complexityId": 2,
    "sourceId": 1,
    "stepCount": 55,
    "userProfileId": 1,
    "modelImg": "flapping_bird_updated.png",
    "artist": "Unknown Artist",
    "modelPapers": [
        { "paperId": 1 },
        { "paperId": 3 }
    ]
    }
    */
    [HttpPut("{id}")]
    //[Authorize]
    public IActionResult UpdateModel(int id, [FromBody] ModelDTO modelDTO)
    {
        if (id != modelDTO.Id)
        {
            return BadRequest("ID mismatch");
        }

        Model existingModel = _dbContext.Models
            .Include(m => m.ModelPapers)
            .SingleOrDefault(m => m.Id == id);

        if (existingModel == null)
        {
            return NotFound();
        }

        existingModel.Title = modelDTO.Title;
        existingModel.ComplexityId = modelDTO.ComplexityId;
        existingModel.SourceId = modelDTO.SourceId;
        existingModel.StepCount = modelDTO.StepCount;
        existingModel.ModelImg = modelDTO.ModelImg;
        existingModel.Artist = modelDTO.Artist;

        _dbContext.ModelPapers.RemoveRange(existingModel.ModelPapers);
        existingModel.ModelPapers = modelDTO.ModelPapers?.Select(mp => new ModelPaper
        {
            ModelId = modelDTO.Id,
            PaperId = mp.PaperId
        }).ToList() ?? new List<ModelPaper>();

        _dbContext.SaveChanges();
        return NoContent();
    }

    // GET: api/model/user/{userId}
    [HttpGet("user/{userId}")]
    //[Authorize]
    public IActionResult GetModelsByUserId(int userId)
    {
        List<Model> models = _dbContext.Models
            .Include(m => m.UserProfile)
            .Include(m => m.Complexity)
            .Include(m => m.Source)
            .Include(m => m.ModelPapers)
                .ThenInclude(mp => mp.Paper)
            .Where(m => m.UserProfileId == userId)
            .OrderByDescending(m => m.CreatedAt)
            .ToList();

        if (!models.Any())
        {
            return NotFound($"No models found for userId: {userId}");
        }

        return Ok(models.Select(MapToDTO).ToList());
    }

    // GET: api/model/recent
    [HttpGet("recent")]
    //[Authorize]
    public IActionResult GetRecentModels()
    {
        List<Model> recentModels = _dbContext.Models
            .Include(m => m.UserProfile)
            .Include(m => m.Complexity)
            .Include(m => m.Source)
            .Include(m => m.ModelPapers)
                .ThenInclude(mp => mp.Paper)
            .OrderByDescending(m => m.CreatedAt)
            .Take(3)
            .ToList();

        return Ok(recentModels.Select(MapToDTO).ToList());
    }

    [HttpPost("upload")]
    //[Authorize]
    [RequestSizeLimit(10_000_000)] // 10MB limit
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is required.");
        }

        var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");

        if (!Directory.Exists(imagesPath))
        {
            Directory.CreateDirectory(imagesPath);
        }

        var fileName = Path.GetFileName(file.FileName);
        var filePath = Path.Combine(imagesPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { fileName });
    }


}