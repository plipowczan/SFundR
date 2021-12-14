using Autofac;
using SFundR.Core.Interfaces;
using SFundR.Core.Services;

namespace SFundR.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<TimeItemSearchService>()
      .As<ITimeItemSearchService>().InstancePerLifetimeScope();
  }
}
