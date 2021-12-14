using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SFundR.Core.ProjectAggregate;
using SFundR.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SFundR.Web.Endpoints.ProjectEndpoints;

public class Update : BaseAsyncEndpoint.WithRequest<UpdateProjectRequest>.WithResponse<UpdateProjectResponse>
{
  private readonly IRepository<Project> _repository;

  public Update(IRepository<Project> repository)
  {
    _repository = repository;
  }

  [HttpPut(UpdateProjectRequest.Route)]
  [SwaggerOperation(
    Summary = "Updates a Project",
    Description = "Updates a Project with a longer description",
    OperationId = "Projects.Update",
    Tags = new[] {"ProjectEndpoints"})
  ]
  public override async Task<ActionResult<UpdateProjectResponse>> HandleAsync(UpdateProjectRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.Name == null)
    {
      return BadRequest();
    }

    var existingProject = await _repository.GetByIdAsync(request.Id, cancellationToken);

    if (existingProject == null)
    {
      return NotFound();
    }

    existingProject.Update(request.Name, request.Description);

    await _repository.UpdateAsync(existingProject, cancellationToken);

    var response = new UpdateProjectResponse(
      new ProjectRecord(existingProject.Id, existingProject.Name)
    );
    return Ok(response);
  }
}
