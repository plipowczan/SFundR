using Ardalis.ApiEndpoints;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using SFundR.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace SFundR.Web.Endpoints.ProjectEndpoints;

public class ListIncomplete : BaseAsyncEndpoint.WithRequest<ListIncompleteRequest>.WithResponse<ListIncompleteResponse>
{
  private readonly ITimeItemSearchService _searchService;

  public ListIncomplete(ITimeItemSearchService searchService)
  {
    _searchService = searchService;
  }

  [HttpGet("/Projects/{ProjectId}/IncompleteItems")]
  [SwaggerOperation(
    Summary = "Gets a list of a project's incomplete items",
    Description = "Gets a list of a project's incomplete items",
    OperationId = "Project.ListIncomplete",
    Tags = new[] {"ProjectEndpoints"})
  ]
  public override async Task<ActionResult<ListIncompleteResponse>> HandleAsync(
    [FromQuery] ListIncompleteRequest request, CancellationToken cancellationToken)
  {
    if (request.SearchString == null)
    {
      return BadRequest();
    }

    var response = new ListIncompleteResponse(0, new List<TimeItemRecord>());
    var result = await _searchService.GetAllUnapprovedItemsAsync(request.ProjectId, request.SearchString);

    if (result.Status == ResultStatus.Ok)
    {
      response.ProjectId = request.ProjectId;
      response.IncompleteItems = new List<TimeItemRecord>(
        result.Value.Select(
          item => new TimeItemRecord(item.Id,
            item.Comment,
            item.Date,
            item.IsApproved, item.ApprovedDateTime)));
    }
    else if (result.Status == ResultStatus.Invalid)
    {
      return BadRequest(result.ValidationErrors);
    }
    else if (result.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }

    return Ok(response);
  }
}
