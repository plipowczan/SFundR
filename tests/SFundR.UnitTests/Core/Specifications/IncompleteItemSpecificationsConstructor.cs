using SFundR.Core.ProjectAggregate;
using SFundR.Core.ProjectAggregate.Specifications;
using Xunit;

namespace SFundR.UnitTests.Core.Specifications;

public class UnapprovedItemsSpecificationConstructor
{
  [Fact]
  public void FilterCollectionToOnlyReturnItemsWithIsApprovedFalse()
  {
    var item1 = new TimeItem();
    var item2 = new TimeItem();
    var item3 = new TimeItem();
    item3.Approve();

    var items = new List<TimeItem> {item1, item2, item3};

    var spec = new UnapprovedItemsSpec();
    var filteredList = items
      .Where(spec.WhereExpressions.First().Compile())
      .ToList();

    Assert.Contains(item1, filteredList);
    Assert.Contains(item2, filteredList);
    Assert.DoesNotContain(item3, filteredList);
  }
}
