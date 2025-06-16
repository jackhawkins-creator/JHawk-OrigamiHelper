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
    public IActionResult GetAllPosts()
    {
        return Ok(_dbContext
            .Models
            .OrderByDescending(m => m.CreatedAt)
            .ToList());
    }
}