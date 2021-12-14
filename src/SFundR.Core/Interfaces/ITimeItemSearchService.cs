using Ardalis.Result;
using SFundR.Core.ProjectAggregate;

namespace SFundR.Core.Interfaces;

public interface ITimeItemSearchService
{
  Task<Result<TimeItem>> GetNextUnapprovedItemAsync(int projectId);
  Task<Result<List<TimeItem>>> GetAllUnapprovedItemsAsync(int projectId, string searchString);
}
