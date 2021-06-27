using System.Net.Http;
using Autofac;
using PriceConverter.Core.Clients;
using PriceConverter.Core.Services;

namespace PriceConverter.Core.Ioc
{
    public static class AutofacModuleRegistration
    {
         public static void RegisterClients(this ContainerBuilder builder)
                {
                    builder.RegisterType<TrainlineExchangeHandler>()
                        .As<ITrainlineExchangeHandler>()
                        .WithParameter(
                            (p, ctx) => p.ParameterType == typeof(HttpClient),
                            (p, ctx) => ctx.Resolve<IHttpClientFactory>().CreateClient(nameof(HttpClient))
                        ).InstancePerDependency();
                }
                
                public static void RegisterServices(this ContainerBuilder builder)
                {
                    builder.RegisterType<ExchangeRateService>()
                        .As<IExchangeRateService>()
                        .InstancePerDependency();
                    
                    builder.RegisterType<PriceConverterService>()
                        .As<IPriceConverterService>()
                        .InstancePerDependency();
                    
                    builder.RegisterType<TrainlineExchangeHandler>()
                        .As<ITrainlineExchangeHandler>()
                        .InstancePerDependency();
                }
    }
}