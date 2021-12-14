using SFundR.SharedKernel;

namespace SFundR.Core.ProjectAggregate.Events;

public class NewItemAddedEvent : BaseDomainEvent
{
  public NewItemAddedEvent(Project project,
    TimeItem newItem)
  {
    Project = project;
    NewItem = newItem;
  }

  public TimeItem NewItem { get; set; }
  public Project Project { get; set; }
}
