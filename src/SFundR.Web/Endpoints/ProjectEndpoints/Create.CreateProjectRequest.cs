using System.ComponentModel.DataAnnotations;

namespace SFundR.Web.Endpoints.ProjectEndpoints;

public class CreateProjectRequest
{
  public const string Route = "/Projects";

  [Required] public string? Name { get; set; }

  public string? Description { get; set; }
}
