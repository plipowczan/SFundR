using Microsoft.AspNetCore.Mvc;
using SFundR.Core.ProjectAggregate;
using SFundR.Core.ProjectAggregate.Specifications;
using SFundR.SharedKernel.Interfaces;
using SFundR.Web.ApiModels;

namespace SFundR.Web.Api;

/// <summary>
///   A sample API Controller. Consider using API Endpoints (see Endpoints folder) for a more SOLID approach to building
///   APIs
///   https://github.com/ardalis/ApiEndpoints
/// </summary>
public class ProjectsController : BaseApiController
{
  private readonly IRepository<Project> _repository;

  public ProjectsController(IRepository<Project> repository)
  {
    _repository = repository;
  }

  // GET: api/Projects
  [HttpGet]
  public async Task<IActionResult> List()
  {
    var projectDTOs = (await _repository.ListAsync())
      .Select(project => new ProjectDTO
      (
        project.Id,
        project.Name,
        project.Description
      ))
      .ToList();

    return Ok(projectDTOs);
  }

  // GET: api/Projects
  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetById(int id)
  {
    var projectSpec = new ProjectByIdWithItemsSpec(id);
    var project = await _repository.GetBySpecAsync(projectSpec);
    if (project == null)
    {
      return NotFound();
    }

    var result = new ProjectDTO
    (
      project.Id,
      project.Name,
      project.Description,
      new List<TimeItemDTO>
      (
        project.Items.Select(TimeItemDTO.FromTimeItem).ToList()
      )
    );

    return Ok(result);
  }

  // POST: api/Projects
  [HttpPost]
  public async Task<IActionResult> Post([FromBody] CreateProjectDTO request)
  {
    var newProject = new Project(request.Name, request.Description);

    var createdProject = await _repository.AddAsync(newProject);

    var result = new ProjectDTO
    (
      createdProject.Id,
      createdProject.Name,
      createdProject.Description
    );
    return Ok(result);
  }

  // PATCH: api/Projects/{projectId}/approve/{itemId}
  [HttpPatch("{projectId:int}/approve/{itemId}")]
  public async Task<IActionResult> Approve(int projectId, int itemId)
  {
    var projectSpec = new ProjectByIdWithItemsSpec(projectId);
    var project = await _repository.GetBySpecAsync(projectSpec);
    if (project == null)
    {
      return NotFound("No such project");
    }

    var timeItem = project.Items.FirstOrDefault(item => item.Id == itemId);
    if (timeItem == null)
    {
      return NotFound("No such item.");
    }

    timeItem.Approve();
    await _repository.UpdateAsync(project);

    return Ok();
  }
}
