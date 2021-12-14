using SFundR.Core.ProjectAggregate.Events;
using Xunit;

namespace SFundR.UnitTests.Core.ProjectAggregate;

public class TimeItemMarkApproved
{
  [Fact]
  public void SetsIsApprovedToTrue()
  {
    var item = new TimeItemBuilder()
      .WithDefaultValues()
      .Comment("")
      .Date(DateTime.Now)
      .WorkTimeHours(1M)
      .Build();

    item.Approve();

    Assert.True(item.IsApproved);
  }

  [Fact]
  public void RaisesTimeItemApprovedEvent()
  {
    var item = new TimeItemBuilder().Build();

    item.Approve();

    Assert.Single(item.Events);
    Assert.IsType<TimeItemApprovedEvent>(item.Events.First());
  }
}
