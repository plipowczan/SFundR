using Ardalis.Specification;

namespace SFundR.Core.ProjectAggregate.Specifications;

public class UnapprovedItemsSearchSpec : Specification<TimeItem>
{
  public UnapprovedItemsSearchSpec(string searchString)
  {
    Query
      .Where(item => !item.IsApproved &&
                     item.Comment.Contains(searchString));
  }
}
