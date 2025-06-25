using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrigamiHelper.Data;
using Microsoft.EntityFrameworkCore;
using OrigamiHelper.Models;
using OrigamiHelper.Models.DTOs;

namespace OrigamiHelper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResponseController : ControllerBase
{
    private OrigamiHelperDbContext _dbContext;

    public ResponseController(OrigamiHelperDbContext context)
    {
        _dbContext = context;
    }

    // GET responses for a specific help request
    [HttpGet("request/{requestId}")]
    //[Authorize]
    public IActionResult GetResponsesByRequestId(int requestId)
    {
        List<ResponseDTO> responses = _dbContext.Responses
            .Include(r => r.Request)
            .Where(r => r.RequestId == requestId)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new ResponseDTO
            {
                Id = r.Id,
                RequestId = r.RequestId,
                ResponderId = r.ResponderId,
                Media = r.Media,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                Request = new RequestDTO
                {
                    Id = r.Request.Id,
                    UserId = r.Request.UserId,
                    ModelId = r.Request.ModelId,
                    StepNumber = r.Request.StepNumber,
                    Description = r.Request.Description,
                    CreatedAt = r.Request.CreatedAt,
                    Model = null,            // Leave these null
                    UserProfile = null
                }
            })
            .ToList();

        return Ok(responses);
    }

    //POST Response
    //Raw Body Test Data:
    /*
    {
    "requestId": 3,
    "responderId": 2,
    "media": "https://www.youtube.com/watch?v=zkePx8Blr5I&pp=ygUTc3F1YXNoIGZvbGQgb3JpZ2FtadIHCQnQCQGHKiGM7w%3D%3D",
    "description": "You don’t need to squash all the way. Just collapse the top triangle like this video shows."
    }
    */
    [HttpPost]
    //[Authorize]
    public IActionResult CreateResponse(ResponseDTO responseDTO)
    {
        if (responseDTO == null)
        {
            return BadRequest("ResponseDTO is required");
        }

        Response response = new Response
        {
            RequestId = responseDTO.RequestId,
            ResponderId = responseDTO.ResponderId,
            Media = responseDTO.Media,
            Description = responseDTO.Description,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Responses.Add(response);
        _dbContext.SaveChanges();

        return Created($"/api/response/{response.Id}", response);
    }

    //DELETE Response
    [HttpDelete("{id}")]
    //[Authorize]
    public IActionResult DeleteResponse(int id)
    {
        Response response = _dbContext.Responses.SingleOrDefault(r => r.Id == id);
        if (response == null)
        {
            return NotFound();
        }

        _dbContext.Responses.Remove(response);
        _dbContext.SaveChanges();

        return NoContent();
    }

}