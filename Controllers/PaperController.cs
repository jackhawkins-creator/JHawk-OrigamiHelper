using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrigamiHelper.Data;
using Microsoft.EntityFrameworkCore;
using OrigamiHelper.Models;
using OrigamiHelper.Models.DTOs;

namespace OrigamiHelper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaperController : ControllerBase
{
    private OrigamiHelperDbContext _dbContext;

    public PaperController(OrigamiHelperDbContext context)
    {
        _dbContext = context;
    }

    //GET all papers
    [HttpGet]
    //[Authorize]
    public IActionResult GetAllPapers()
    {
        List<PaperDTO> papers = _dbContext.Papers
            .Select(p => new PaperDTO
            {
                Id = p.Id,
                Brand = p.Brand
            })
            .ToList();

        return Ok(papers);
    }

    //GET single Paper by id
    [HttpGet("papers/{id}")]
    //[Authorize]
    public IActionResult GetPaperById(int id)
    {
        PaperDTO paper = _dbContext.Papers
            .Where(p => p.Id == id)
            .Select(p => new PaperDTO
            {
                Id = p.Id,
                Brand = p.Brand
            })
            .FirstOrDefault();

        if (paper == null)
        {
            return NotFound();
        }

        return Ok(paper);
    }
}