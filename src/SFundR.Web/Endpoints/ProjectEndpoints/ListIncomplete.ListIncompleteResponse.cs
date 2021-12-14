namespace SFundR.Web.Endpoints.ProjectEndpoints;

public class ListIncompleteResponse
{
  public ListIncompleteResponse(int projectId, List<TimeItemRecord> incompleteItems)
  {
    ProjectId = projectId;
    IncompleteItems = incompleteItems;
  }

  public int ProjectId { get; set; }
  public List<TimeItemRecord> IncompleteItems { get; set; }
}
