namespace SFundR.Web.Endpoints.ProjectEndpoints;

public class GetProjectByIdResponse
{
  public GetProjectByIdResponse(int id, string name, List<TimeItemRecord> items)
  {
    Id = id;
    Name = name;
    Items = items;
  }

  public int Id { get; set; }
  public string Name { get; set; }
  public List<TimeItemRecord> Items { get; set; } = new();
}
