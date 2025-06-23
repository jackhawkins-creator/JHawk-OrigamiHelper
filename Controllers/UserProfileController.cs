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
    [HttpGet("{id}")]
    [Authorize]
    public IActionResult GetUserProfileById(int id)
    {
        UserProfileDTO profile = _dbContext.UserProfiles
            .Include(up => up.IdentityUser)
            .Where(up => up.Id == id)
            .Select(up => new UserProfileDTO
            {
                Id = up.Id,
                FirstName = up.FirstName,
                LastName = up.LastName,
                Address = up.Address,
                Email = up.IdentityUser.Email,
                UserName = up.IdentityUser.UserName,
                IdentityUserId = up.IdentityUserId
            })
            .FirstOrDefault();

        if (profile == null)
        {
            return NotFound();
        }

        return Ok(profile);
    }

    //PUT Update profile
    //Raw Body Test Data:
    /*
    {
    "firstName": "Ada",
    "lastName": "Lovelace",
    "address": "123 Mathematics Blvd"
    }
    */
    [HttpPut("me")]
    [Authorize]
    public IActionResult UpdateCurrentUserProfile([FromBody] UserProfileDTO updatedProfileDto)
    {
        string firebaseId = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        if (firebaseId == null)
        {
            return Unauthorized();
        }

        UserProfile userProfile = _dbContext.UserProfiles.FirstOrDefault(up => up.IdentityUserId == firebaseId);

        if (userProfile == null)
        {
            return NotFound("User profile not found.");
        }

        userProfile.FirstName = updatedProfileDto.FirstName;
        userProfile.LastName = updatedProfileDto.LastName;
        userProfile.Address = updatedProfileDto.Address;

        _dbContext.SaveChanges();

        return NoContent();
    }

}