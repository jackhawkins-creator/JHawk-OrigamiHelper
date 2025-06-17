using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrigamiHelper.Data;
using Microsoft.EntityFrameworkCore;
using OrigamiHelper.Models;
using OrigamiHelper.Models.DTOs;

namespace OrigamiHelper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SourceController : ControllerBase
{
    private OrigamiHelperDbContext _dbContext;

    public SourceController(OrigamiHelperDbContext context)
    {
        _dbContext = context;
    }

    //GET all sources
    [HttpGet]
    //[Authorize]
    public IActionResult GetAllSources()
    {
        return Ok(_dbContext
            .Sources
            .ToList());
    }
}