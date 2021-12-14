using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Result;
using SFundR.Core.ProjectAggregate;

namespace SFundR.Core.Interfaces;

public interface IToDoItemSearchService
{
  Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId);
  Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString);
}
