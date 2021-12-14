using SFundR.Core.ProjectAggregate;

namespace SFundR.Web.ViewModels;

public class TimeItemViewModel
{
  public int Id { get; set; }

  /// <summary>
  ///   Komentarz
  /// </summary>
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

  /// <summary>
  ///   Data zatwierdzenia
  /// </summary>
  public DateTime ApprovedDateTime { get; set; }

  public static TimeItemViewModel FromTimeItem(TimeItem item)
  {
    return new TimeItemViewModel
    {
      Id = item.Id,
      Comment = item.Comment,
      Date = item.Date,
      WorkTimeHours = item.WorkTimeHours,
      IsApproved = item.IsApproved,
      ApprovedDateTime = item.ApprovedDateTime
    };
  }
}
