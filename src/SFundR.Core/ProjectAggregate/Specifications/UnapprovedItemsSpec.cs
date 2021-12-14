using Ardalis.Specification;

namespace SFundR.Core.ProjectAggregate.Specifications;

public class UnapprovedItemsSpec : Specification<TimeItem>
{
  public UnapprovedItemsSpec()
  {
    Query.Where(item => !item.IsApproved);
  }
}
