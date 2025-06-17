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
        return Ok(_dbContext
            .Papers
            .ToList());
    }
}