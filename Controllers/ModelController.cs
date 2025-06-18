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
    "artist": "Unknown"
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

        _dbContext.Models.Add(model);
        _dbContext.SaveChanges();

        return Created($"/api/model/{model.Id}", model);
    }

    //DELETE Model
    [HttpDelete("{id}")]
    //[Authorize]
    public IActionResult DeleteModel(int id)
    {
        Model model = _dbContext.Models.SingleOrDefault(o => o.Id == id);

        if (model == null)
        {
            return NotFound();
        }

        _dbContext.Models.Remove(model);
        _dbContext.SaveChanges();

        return NoContent();
    }

}