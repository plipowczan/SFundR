using SFundR.Core.ProjectAggregate;

namespace SFundR.UnitTests;

// Learn more about test builders:
// https://ardalis.com/improve-tests-with-the-builder-pattern-for-test-data
public class TimeItemBuilder
{
  private TimeItem _time = new();

  public TimeItemBuilder Id(int id)
  {
    _time.Id = id;
    return this;
  }

  public TimeItemBuilder Comment(string comment)
  {
    _time.Comment = comment;
    return this;
  }

  public TimeItemBuilder Date(DateTime date)
  {
    _time.Date = date;
    return this;
  }

  public TimeItemBuilder WorkTimeHours(decimal workTimeHours)
  {
    _time.WorkTimeHours = workTimeHours;
    return this;
  }

  public TimeItemBuilder WithDefaultValues()
  {
    _time = new TimeItem {Id = 1, Comment = "Test Item", Date = new DateTime(2000, 1, 1)};

    return this;
  }

  public TimeItem Build()
  {
    return _time;
  }
}
