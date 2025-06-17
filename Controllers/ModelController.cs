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
            .Include(m => m.Paper)
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

}