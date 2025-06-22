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

    //GET all models, newest to oldest
    [HttpGet]
    //[Authorize]
    public IActionResult GetAllModels()
    {
        return Ok(_dbContext
            .Models
            .OrderByDescending(m => m.CreatedAt)
            .ToList());
    }

    //GET single Model by id
    [HttpGet("{id}")]
    //[Authorize]
    public IActionResult GetById(int id)
    {
        Model model = _dbContext
            .Models
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

        return Ok(model);
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
    public IActionResult CreateModel([FromBody] Model model)
    {
        if (model == null)
        {
            return BadRequest("Model data is required");
        }

        model.CreatedAt = DateTime.UtcNow;

        // Detach any linked Paper entities if present (EF Core will track them otherwise)
        if (model.ModelPapers != null)
        {
            foreach (ModelPaper mp in model.ModelPapers)
            {
                mp.Paper = null; // Avoid trying to insert/update Paper entity
            }
        }

        _dbContext.Models.Add(model);
        _dbContext.SaveChanges();

        return Created($"/api/model/{model.Id}", model);
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

        // Get the authenticated user's IdentityUserId (if using Identity)
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
    public IActionResult UpdateModel(int id, [FromBody] Model model)
    {
        if (id != model.Id)
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

        // Update scalar properties
        existingModel.Title = model.Title;
        existingModel.ComplexityId = model.ComplexityId;
        existingModel.SourceId = model.SourceId;
        existingModel.StepCount = model.StepCount;
        existingModel.ModelImg = model.ModelImg;
        existingModel.Artist = model.Artist;

        // Replace ModelPapers (clear and re-add)
        _dbContext.ModelPapers.RemoveRange(existingModel.ModelPapers);

        if (model.ModelPapers != null && model.ModelPapers.Any())
        {
            foreach (ModelPaper mp in model.ModelPapers)
            {
                existingModel.ModelPapers.Add(new ModelPaper
                {
                    ModelId = model.Id,
                    PaperId = mp.PaperId
                });
            }
        }

        _dbContext.SaveChanges();

        return NoContent();
    }

    // GET: api/model/user/{userId}
    [HttpGet("user/{userId}")]
    //[Authorize]
    public IActionResult GetModelsByUserId(int userId)
    {
        var models = _dbContext.Models
            .Include(m => m.UserProfile)
            .Include(m => m.Complexity)
            .Include(m => m.Source)
            .Include(m => m.ModelPapers)
                .ThenInclude(mp => mp.Paper)
            .Where(m => m.UserProfileId == userId)
            .OrderByDescending(m => m.CreatedAt)
            .ToList();

        if (models == null || !models.Any())
        {
            return NotFound($"No models found for userId: {userId}");
        }

        return Ok(models);
    }

    // GET: api/model/recent
    [HttpGet("recent")]
    //[Authorize]
    public IActionResult GetRecentModels()
    {
        List<Model> recentModels = _dbContext.Models
            .OrderByDescending(m => m.CreatedAt)
            .Take(3)
            .ToList();

        return Ok(recentModels);
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