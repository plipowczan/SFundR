using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFundR.Core.ProjectAggregate;
using SFundR.Core.ProjectAggregate.Specifications;
using SFundR.SharedKernel.Interfaces;
using SFundR.Web.ApiModels;

namespace SFundR.Web.Pages.ProjectDetails;

public class IndexModel : PageModel
{
  private readonly IRepository<Project> _projectRepository;

  public IndexModel(IRepository<Project> projectRepository)
  {
    _projectRepository = projectRepository;
  }

  [BindProperty(SupportsGet = true)] public int ProjectId { get; set; }

  public string Message { get; set; } = "";

  public ProjectDTO? Project { get; set; }

  public async Task OnGetAsync()
  {
    var projectSpec = new ProjectByIdWithItemsSpec(ProjectId);
    var project = await _projectRepository.GetBySpecAsync(projectSpec);

    if (project == null)
    {
      Message = "No project found.";
      return;
    }

    Project = new ProjectDTO
    (
      project.Id,
      project.Name,
      project.Description,
      project.Items
        .Select(TimeItemDTO.FromTimeItem)
        .ToList()
    );
  }
}
