using Microsoft.EntityFrameworkCore;
using SFundR.Core.ProjectAggregate;
using Xunit;

namespace SFundR.IntegrationTests.Data;

public class EfRepositoryUpdate : BaseEfRepoTestFixture
{
  [Fact]
  public async Task UpdatesItemAfterAddingIt()
  {
    // add a project
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var initialDescription = Guid.NewGuid().ToString();
    var project = new Project(initialName, initialDescription);

    await repository.AddAsync(project);

    // detach the item so we get a different instance
    _dbContext.Entry(project).State = EntityState.Detached;

    // fetch the item and update its title
    var newProject = (await repository.ListAsync())
      .FirstOrDefault(project => project.Name == initialName);
    if (newProject == null)
    {
      Assert.NotNull(newProject);
      return;
    }

    Assert.NotSame(project, newProject);
    var newName = Guid.NewGuid().ToString();
    var newDescription = Guid.NewGuid().ToString();
    newProject.Update(newName, newDescription);

    // Update the item
    await repository.UpdateAsync(newProject);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
      .FirstOrDefault(project => project.Name == newName);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(project.Name, updatedItem?.Name);
    Assert.Equal(newProject.Id, updatedItem?.Id);
  }
}
