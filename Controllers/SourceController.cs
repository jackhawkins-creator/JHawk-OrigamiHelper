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
        List<SourceDTO> sources = _dbContext.Sources
            .Select(s => new SourceDTO
            {
                Id = s.Id,
                Title = s.Title
            })
            .ToList();

        return Ok(sources);
    }

    //GET single source by id
    [HttpGet("sources/{id}")]
    //[Authorize]
    public IActionResult GetSourceById(int id)
    {
        SourceDTO source = _dbContext.Sources
            .Where(s => s.Id == id)
            .Select(s => new SourceDTO
            {
                Id = s.Id,
                Title = s.Title
            })
            .FirstOrDefault();

        if (source == null)
        {
            return NotFound();
        }

        return Ok(source);
    }
}