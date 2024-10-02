using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using TaskTrackerFrontend.ViewModels;
using TaskTrackerFrontend.Services;

namespace TaskTrackerFrontend
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var services = new ServiceCollection();

                ConfigureServices(services);

                ServiceProvider = services.BuildServiceProvider();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = ServiceProvider.GetRequiredService<TaskViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var apiUrl = Environment.GetEnvironmentVariable("API_URL") ?? throw new InvalidOperationException("API_URL environment variable is not set.");

            services.AddHttpClient<ITaskService, TaskService>(client =>
            {
                client.BaseAddress = new Uri(apiUrl);
            });

            services.AddTransient<TaskViewModel>();
        }
    }
}