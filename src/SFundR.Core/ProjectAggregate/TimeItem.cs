using SFundR.Core.ProjectAggregate.Events;
using SFundR.SharedKernel;

namespace SFundR.Core.ProjectAggregate;

public class TimeItem : BaseEntity
{
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
  public decimal WorkTimeHours { get; set; } = 0M;

  /// <summary>
  ///   Czy czas jest zatwierdzony
  /// </summary>
  public bool IsApproved { get; set; }

  /// <summary>
  ///   Data zatwierdzenia
  /// </summary>
  public DateTime ApprovedDateTime { get; set; }

  public void Approve()
  {
    if (!IsApproved)
    {
      IsApproved = true;
      ApprovedDateTime = DateTime.Now;

      Events.Add(new TimeItemApprovedEvent(this));
    }
  }

  public override string ToString()
  {
    var status = IsApproved ? "Approved!" : "Not approved.";
    return $"{Id}: Status: {status} - {Date:yyyy-MM-dd} - {Comment} - Hours: {WorkTimeHours}";
  }
}
