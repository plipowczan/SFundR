using SFundR.Core.ProjectAggregate;
using Xunit;

namespace SFundR.UnitTests.Core.ProjectAggregate;

public class ProjectConstructor
{
  private const string TestName = "test name";
  private const string TestDescription = "test description";
  private Project? _testProject;

  private Project CreateProject()
  {
    return new Project(TestName, TestDescription);
  }

  [Fact]
  public void InitializesName()
  {
    _testProject = CreateProject();

    Assert.Equal(TestName, _testProject.Name);
  }

  [Fact]
  public void InitializesTaskListToEmptyList()
  {
    _testProject = CreateProject();

    Assert.NotNull(_testProject.Items);
  }
}
