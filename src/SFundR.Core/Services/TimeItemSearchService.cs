using Ardalis.Result;
using SFundR.Core.Interfaces;
using SFundR.Core.ProjectAggregate;
using SFundR.Core.ProjectAggregate.Specifications;
using SFundR.SharedKernel.Interfaces;

namespace SFundR.Core.Services;

public class TimeItemSearchService : ITimeItemSearchService
{
  private readonly IRepository<Project> _repository;

  public TimeItemSearchService(IRepository<Project> repository)
  {
    _repository = repository;
  }

  public async Task<Result<List<TimeItem>>> GetAllUnapprovedItemsAsync(int projectId, string searchString)
  {
    if (string.IsNullOrEmpty(searchString))
    {
      var errors = new List<ValidationError>
      {
        new() {Identifier = nameof(searchString), ErrorMessage = $"{nameof(searchString)} is required."}
      };
      return Result<List<TimeItem>>.Invalid(errors);
    }

    var projectSpec = new ProjectByIdWithItemsSpec(projectId);
    var project = await _repository.GetBySpecAsync(projectSpec);

    // TODO: Optionally use Ardalis.GuardClauses Guard.Against.NotFound and catch
    if (project == null)
    {
      return Result<List<TimeItem>>.NotFound();
    }

    var incompleteSpec = new UnapprovedItemsSearchSpec(searchString);

    try
    {
      var items = incompleteSpec.Evaluate(project.Items).ToList();

      return new Result<List<TimeItem>>(items);
    }
    catch (Exception ex)
    {
      // TODO: Log details here
      return Result<List<TimeItem>>.Error(ex.Message);
    }
  }

  public async Task<Result<TimeItem>> GetNextUnapprovedItemAsync(int projectId)
  {
    var projectSpec = new ProjectByIdWithItemsSpec(projectId);
    var project = await _repository.GetBySpecAsync(projectSpec);
    if (project == null)
    {
      return Result<TimeItem>.NotFound();
    }

    var incompleteSpec = new UnapprovedItemsSpec();

    var items = incompleteSpec.Evaluate(project.Items).ToList();

    if (!items.Any())
    {
      return Result<TimeItem>.NotFound();
    }

    return new Result<TimeItem>(items.First());
  }
}
