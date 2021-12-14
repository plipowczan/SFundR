using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SFundR.Core.ProjectAggregate;
using SFundR.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SFundR.Web.Endpoints.ProjectEndpoints;

public class Create : BaseAsyncEndpoint.WithRequest<CreateProjectRequest>.WithResponse<CreateProjectResponse>
{
  private readonly IRepository<Project> _projectRepository;

  public Create(IRepository<Project> projectRepository)
  {
    _projectRepository = projectRepository;
  }

  [HttpPost("/Projects")]
  [SwaggerOperation(
    Summary = "Creates a new Project",
    Description = "Creates a new Project",
    OperationId = "Project.Create",
    Tags = new[] {"ProjectEndpoints"})
  ]
  public override async Task<ActionResult<CreateProjectResponse>> HandleAsync(CreateProjectRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.Name == null)
    {
      return BadRequest();
    }

    var newProject = new Project(request.Name, request.Description);

    var createdItem = await _projectRepository.AddAsync(newProject, cancellationToken);

    var response = new CreateProjectResponse
    (
      createdItem.Id,
      createdItem.Name,
      createdItem.Description
    );

    return Ok(response);
  }
}
