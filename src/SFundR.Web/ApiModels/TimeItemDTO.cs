using System.ComponentModel.DataAnnotations;
using SFundR.Core.ProjectAggregate;

namespace SFundR.Web.ApiModels;

public class TimeItemDTO
{
  public int Id { get; set; }

  /// <summary>
  ///   Komentarz
  /// </summary>
  [Required]
  public string Comment { get; set; } = string.Empty;

  /// <summary>
  ///   Data
  /// </summary>
  public DateTime Date { get; set; }

  /// <summary>
  ///   liczba godzin
  /// </summary>
  public decimal WorkTimeHours { get; set; }

  /// <summary>
  ///   Czy czas jest zatwierdzony
  /// </summary>
  public bool IsApproved { get; set; }

  public static TimeItemDTO FromTimeItem(TimeItem item)
  {
    return new TimeItemDTO
    {
      Id = item.Id,
      Comment = item.Comment,
      Date = item.Date,
      WorkTimeHours = item.WorkTimeHours,
      IsApproved = item.IsApproved
    };
  }
}
