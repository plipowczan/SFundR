using Microsoft.EntityFrameworkCore;
using SFundR.Core.ProjectAggregate;
using SFundR.Infrastructure.Data;

namespace SFundR.Web;

public static class SeedData
{
  public static readonly Project TestProject1 = new("Test Project", "Test Project Description");

  public static readonly TimeItem TimeItem1 = new()
  {
    Comment = "Get Sample Working", Date = DateTime.Now.AddDays(-1), WorkTimeHours = 1
  };

  public static readonly TimeItem TimeItem2 = new()
  {
    Comment = "Review Solution", Date = DateTime.Now.AddDays(-2), WorkTimeHours = 1.5M
  };

  public static readonly TimeItem TimeItem3 = new()
  {
    Comment = "Run and Review Tests", Date = DateTime.Now.AddDays(-3), WorkTimeHours = 2
  };

  public static void Initialize(IServiceProvider serviceProvider)
  {
    using var dbContext = new AppDbContext(
      serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null);
    // Look for any time items.
    if (dbContext.Times.Any())
    {
      return; // DB has been seeded
    }

    PopulateTestData(dbContext);
  }

  public static void PopulateTestData(AppDbContext dbContext)
  {
    foreach (var item in dbContext.Times)
    {
      dbContext.Remove(item);
    }

    dbContext.SaveChanges();

    TestProject1.Id = 1;
    TestProject1.AddItem(TimeItem1);
    TestProject1.AddItem(TimeItem2);
    TestProject1.AddItem(TimeItem3);
    dbContext.Projects.Add(TestProject1);

    dbContext.SaveChanges();
  }
}
