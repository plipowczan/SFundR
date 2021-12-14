using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using SFundR.Core.ProjectAggregate;
using SFundR.Core.ProjectAggregate.Specifications;
using SFundR.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SFundR.Web.Endpoints.ProjectEndpoints;

public class GetById : BaseAsyncEndpoint.WithRequest<GetProjectByIdRequest>.WithResponse<GetProjectByIdResponse>
{
  private readonly IRepository<Project> _repository;

  public GetById(IRepository<Project> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetProjectByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a single Project",
    Description = "Gets a single Project by Id",
    OperationId = "Projects.GetById",
    Tags = new[] {"ProjectEndpoints"})
  ]
  public override async Task<ActionResult<GetProjectByIdResponse>> HandleAsync(
    [FromRoute] GetProjectByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ProjectByIdWithItemsSpec(request.ProjectId);
    var entity = await _repository.GetBySpecAsync(spec, cancellationToken); // TODO: pass cancellation token
    if (entity == null)
    {
      return NotFound();
    }

    var response = new GetProjectByIdResponse
    (
      entity.Id,
      entity.Name,
      entity.Items.Select(item =>
        new TimeItemRecord(item.Id, item.Comment, item.Date, item.IsApproved, item.ApprovedDateTime)).ToList()
    );
    return Ok(response);
  }
}
