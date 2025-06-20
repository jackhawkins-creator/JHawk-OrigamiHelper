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
    //[Authorize]
    public IActionResult UpdateCurrentUserProfile([FromBody] UserProfile updatedProfile)
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

        // Update relevant fields only (protect IdentityUserId and Id)
        userProfile.FirstName = updatedProfile.FirstName;
        userProfile.LastName = updatedProfile.LastName;
        userProfile.Address = updatedProfile.Address;

        _dbContext.SaveChanges();

        return NoContent();
    }

}