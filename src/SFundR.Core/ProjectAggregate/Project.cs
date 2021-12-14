using Ardalis.GuardClauses;
using SFundR.Core.ProjectAggregate.Events;
using SFundR.SharedKernel;
using SFundR.SharedKernel.Interfaces;

namespace SFundR.Core.ProjectAggregate;

public class Project : BaseEntity, IAggregateRoot
{
  private readonly List<TimeItem> _items = new();

  public Project(string name, string description)
  {
    Description = description;
    Name = Guard.Against.NullOrEmpty(name, nameof(name));
  }

  public string Name { get; private set; }

  public string Description { get; private set; }
  public IEnumerable<TimeItem> Items => _items.AsReadOnly();

  public void AddItem(TimeItem newItem)
  {
    Guard.Against.Null(newItem, nameof(newItem));
    _items.Add(newItem);

    var newItemAddedEvent = new NewItemAddedEvent(this, newItem);
    Events.Add(newItemAddedEvent);
  }

  public void Update(string newName, string newDescription)
  {
    Name = Guard.Against.NullOrEmpty(newName, nameof(newName));
    Description = newDescription;
  }
}
