using Microsoft.AspNetCore.Mvc.RazorPages;
using SFundR.Core.ProjectAggregate;
using SFundR.Core.ProjectAggregate.Specifications;
using SFundR.SharedKernel.Interfaces;

namespace SFundR.Web.Pages.ProjectDetails;

public class UnapprovedModel : PageModel
{
  private readonly IRepository<Project> _projectRepository;

  public UnapprovedModel(IRepository<Project> projectRepository)
  {
    _projectRepository = projectRepository;
  }

  public List<TimeItem>? TimeItems { get; set; }

  public async Task OnGetAsync()
  {
    var projectSpec = new ProjectByIdWithItemsSpec(1); // TODO: get from route
    var project = await _projectRepository.GetBySpecAsync(projectSpec);
    if (project == null)
    {
      return;
    }

    var spec = new UnapprovedItemsSpec();

    TimeItems = spec.Evaluate(project.Items).ToList();
  }
}
