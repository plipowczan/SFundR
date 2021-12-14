using SFundR.SharedKernel;

namespace SFundR.Core.ProjectAggregate.Events;

public class TimeItemApprovedEvent : BaseDomainEvent
{
  public TimeItemApprovedEvent(TimeItem approvedItem)
  {
    ApprovedItem = approvedItem;
  }

  public TimeItem ApprovedItem { get; set; }
}
