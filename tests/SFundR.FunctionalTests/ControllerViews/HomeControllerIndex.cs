using SFundR.Web;
using Xunit;

namespace SFundR.FunctionalTests.ControllerViews;

[Collection("Sequential")]
public class HomeControllerIndex : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public HomeControllerIndex(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsViewWithCorrectMessage()
  {
    var response = await _client.GetAsync("/");
    response.EnsureSuccessStatusCode();
    var stringResponse = await response.Content.ReadAsStringAsync();

    Assert.Contains("SFundR.Web", stringResponse);
  }
}
