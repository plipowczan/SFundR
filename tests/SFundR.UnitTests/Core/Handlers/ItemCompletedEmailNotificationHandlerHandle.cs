using Moq;
using SFundR.Core.Interfaces;
using SFundR.Core.ProjectAggregate;
using SFundR.Core.ProjectAggregate.Events;
using SFundR.Core.ProjectAggregate.Handlers;
using Xunit;

namespace SFundR.UnitTests.Core.Handlers;

public class ItemCompletedEmailNotificationHandlerHandle
{
  private readonly Mock<IEmailSender> _emailSenderMock;
  private readonly ItemCompletedEmailNotificationHandler _handler;

  public ItemCompletedEmailNotificationHandlerHandle()
  {
    _emailSenderMock = new Mock<IEmailSender>();
    _handler = new ItemCompletedEmailNotificationHandler(_emailSenderMock.Object);
  }

  [Fact]
  public async Task ThrowsExceptionGivenNullEventArgument()
  {
#nullable disable
    Exception ex = await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
#nullable enable
  }

  [Fact]
  public async Task SendsEmailGivenEventInstance()
  {
    await _handler.Handle(new TimeItemApprovedEvent(new TimeItem()), CancellationToken.None);

    _emailSenderMock.Verify(
      sender => sender.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
      Times.Once);
  }
}
