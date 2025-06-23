using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrigamiHelper.Data;
using Microsoft.EntityFrameworkCore;
using OrigamiHelper.Models;
using OrigamiHelper.Models.DTOs;

namespace OrigamiHelper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplexityController : ControllerBase
{
    private OrigamiHelperDbContext _dbContext;

    public ComplexityController(OrigamiHelperDbContext context)
    {
        _dbContext = context;
    }

    //GET all complexities
    [HttpGet]
    [Authorize]
    public IActionResult GetAllComplexities()
    {
        List<ComplexityDTO> complexities = _dbContext.Complexities
            .Select(c => new ComplexityDTO
            {
                Id = c.Id,
                Difficulty = c.Difficulty
            })
            .ToList();

        return Ok(complexities);
    }

    //GET single complexity by id
    [HttpGet("complexities/{id}")]
    [Authorize]
    public IActionResult GetComplexityById(int id)
    {
        ComplexityDTO complexity = _dbContext.Complexities
            .Where(c => c.Id == id)
            .Select(c => new ComplexityDTO
            {
                Id = c.Id,
                Difficulty = c.Difficulty
            })
            .FirstOrDefault();

        if (complexity == null)
        {
            return NotFound();
        }

        return Ok(complexity);
    }
}