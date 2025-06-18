using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrigamiHelper.Data;
using Microsoft.EntityFrameworkCore;
using OrigamiHelper.Models;
using OrigamiHelper.Models.DTOs;

namespace OrigamiHelper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private OrigamiHelperDbContext _dbContext;

    public UserProfileController(OrigamiHelperDbContext context)
    {
        _dbContext = context;
    }

    //GET single profile by id
    [HttpGet("userprofiles/{id}")]
    //[Authorize]
    public IActionResult GetUserProfileById(int id)
    {
        UserProfile userProfile = _dbContext.UserProfiles.FirstOrDefault(p => p.Id == id);
        if (userProfile == null)
        {
            return NotFound();
        }
        return Ok(userProfile);
    }
}