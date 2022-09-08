using AudioTools;
using AudioToolsDemo;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace AudioToolsDemoWithDI;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddAudioServices();
        services.AddTransient<IAudioController, AudioController>();
        services.AddSingleton<MainWindow>();
        services.AddTransient<MainViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainwWindow = _serviceProvider.GetService<MainWindow>();
        mainwWindow?.Show();
    }
}
