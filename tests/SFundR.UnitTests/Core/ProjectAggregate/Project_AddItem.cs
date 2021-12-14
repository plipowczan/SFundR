using SFundR.Core.ProjectAggregate;
using Xunit;

namespace SFundR.UnitTests.Core.ProjectAggregate;

public class Project_AddItem
{
  private readonly Project _testProject = new("some name", "some description");

  [Fact]
  public void AddsItemToItems()
  {
    var testItem = new TimeItem {Comment = "title", Date = DateTime.Now, WorkTimeHours = 2};

    _testProject.AddItem(testItem);

    Assert.Contains(testItem, _testProject.Items);
  }

  [Fact]
  public void ThrowsExceptionGivenNullItem()
  {
#nullable disable
    var action = () => _testProject.AddItem(null);
#nullable enable

    var ex = Assert.Throws<ArgumentNullException>(action);
    Assert.Equal("newItem", ex.ParamName);
  }
}
