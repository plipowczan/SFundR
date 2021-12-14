namespace SFundR.Web.ViewModels;

public class ProjectViewModel
{
  public List<TimeItemViewModel> Items = new();
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
}
