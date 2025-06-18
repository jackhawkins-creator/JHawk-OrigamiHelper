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

    //GET single source by id
    [HttpGet("sources/{id}")]
    //[Authorize]
    public IActionResult GetSourceById(int id)
    {
        Source source = _dbContext.Sources.FirstOrDefault(p => p.Id == id);
        if (source == null)
        {
            return NotFound();
        }
        return Ok(source);
    }
}