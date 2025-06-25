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

}