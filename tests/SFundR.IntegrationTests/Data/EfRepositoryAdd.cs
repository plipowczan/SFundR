using SFundR.Core.ProjectAggregate;
using Xunit;

namespace SFundR.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsProjectAndSetsId()
  {
    const string testProjectName = "testProject";
    const string testProjectDescription = "testProjectDescription";
    var repository = GetRepository();
    var project = new Project(testProjectName, testProjectDescription);

    await repository.AddAsync(project);

    var newProject = (await repository.ListAsync())
      .FirstOrDefault();

    Assert.Equal(testProjectName, newProject?.Name);
    Assert.True(newProject?.Id > 0);
  }
}
