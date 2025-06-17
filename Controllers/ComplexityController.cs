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
    //[Authorize]
    public IActionResult GetAllComplexities()
    {
        return Ok(_dbContext
            .Complexities
            .ToList());
    }
}