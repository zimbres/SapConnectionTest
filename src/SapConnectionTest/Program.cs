using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using SAP.Middleware.Connector;
using SapConnectionTest;
using SapConnectionTest.Configurations;
using SapConnectionTest.Services;
using SapConnectionTest.Services.Mail;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "Sap Connection Test";
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHttpClient("Defalt", client => { })
        .ConfigureHttpClient(client =>
        {
            client.DefaultRequestHeaders.UserAgent
            .ParseAdd($"{hostContext.Configuration.GetSection("ApplicationSettings:UserAgent").Value}/{Assembly.GetExecutingAssembly().GetName().Version}");
        });
        services.AddHostedService<Worker>().Configure<HostOptions>(options =>
        {
            options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });
        services.AddSingleton<MailService>();
        services.AddSingleton<PingTest>();
        services.AddSingleton<ImAliveService>();
        services.RemoveAll<IHttpMessageHandlerBuilderFilter>(); //Disable HttpClient Logging
        RfcDestinationManager.RegisterDestinationConfiguration(new SapDestination(hostContext.Configuration));
        RfcTrace.DefaultTraceLevel = 0;
    })
    .Build();

await host.RunAsync();
