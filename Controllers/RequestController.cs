using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrigamiHelper.Data;
using Microsoft.EntityFrameworkCore;
using OrigamiHelper.Models;
using OrigamiHelper.Models.DTOs;

namespace OrigamiHelper.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RequestController : ControllerBase
{
    private OrigamiHelperDbContext _dbContext;

    public RequestController(OrigamiHelperDbContext context)
    {
        _dbContext = context;
    }

    //GET all requests
    [HttpGet]
    //[Authorize]
    public IActionResult GetAllRequests()
    {
        List<RequestDTO> requests = _dbContext.Requests
            .Include(r => r.Model)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => new RequestDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                ModelId = r.ModelId,
                StepNumber = r.StepNumber,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                Model = r.Model == null ? null : new ModelDTO
                {
                    Id = r.Model.Id,
                    Title = r.Model.Title,
                    StepCount = r.Model.StepCount,
                    UserProfileId = r.Model.UserProfileId,
                    CreatedAt = r.Model.CreatedAt,
                    ModelImg = r.Model.ModelImg,
                    Artist = r.Model.Artist,
                    ComplexityId = r.Model.ComplexityId,
                    SourceId = r.Model.SourceId,
                    // Don't populate Complexity, Source, or ModelPapers per your note
                    UserProfile = null,
                    Complexity = null,
                    Source = null,
                    ModelPapers = new List<ModelPaperDTO>()
                }
            })
            .ToList();

        return Ok(requests);
    }

    //GET single Request by id
    [HttpGet("requests/{id}")]
    //[Authorize]
    public IActionResult GetRequestById(int id)
    {
        RequestDTO request = _dbContext.Requests
            .Include(r => r.Model)
            .Where(r => r.Id == id)
            .Select(r => new RequestDTO
            {
                Id = r.Id,
                UserId = r.UserId,
                ModelId = r.ModelId,
                StepNumber = r.StepNumber,
                Description = r.Description,
                CreatedAt = r.CreatedAt,
                Model = r.Model == null ? null : new ModelDTO
                {
                    Id = r.Model.Id,
                    Title = r.Model.Title,
                    StepCount = r.Model.StepCount,
                    UserProfileId = r.Model.UserProfileId,
                    CreatedAt = r.Model.CreatedAt,
                    ModelImg = r.Model.ModelImg,
                    Artist = r.Model.Artist,
                    ComplexityId = r.Model.ComplexityId,
                    SourceId = r.Model.SourceId,
                    UserProfile = null,
                    Complexity = null,
                    Source = null,
                    ModelPapers = new List<ModelPaperDTO>()
                }
            })
            .FirstOrDefault();

        if (request == null)
        {
            return NotFound();
        }

        return Ok(request);
    }

    //POST Request
    /*
    {
  "userId": 3,
  "modelId": 2,
  "stepNumber": 151,
  "description": "Having trouble interpreting the sink fold direction at this step."
    }
    */
    [HttpPost]
    //[Authorize]
    public IActionResult CreateRequest([FromBody] RequestDTO requestDTO)
    {
        //validate if the model exists
        Model model = _dbContext.Models.Find(requestDTO.ModelId);
        if (model == null)
        {
            return BadRequest("Invalid ModelId.");
        }

        //validate the step number is within range
        if (requestDTO.StepNumber < 1 || requestDTO.StepNumber > model.StepCount)
        {
            return BadRequest("StepNumber is out of valid range for this model.");
        }

        //validate user
        UserProfile user = _dbContext.UserProfiles.Find(requestDTO.UserId);
        if (user == null)
        {
            return BadRequest("Invalid UserId.");
        }

        Request request = new Request
        {
            UserId = requestDTO.UserId,
            ModelId = requestDTO.ModelId,
            StepNumber = requestDTO.StepNumber,
            Description = requestDTO.Description,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Requests.Add(request);
        _dbContext.SaveChanges();

        return Created($"/api/request/{requestDTO.Id}", requestDTO);
    }

}