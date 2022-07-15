using KebabApplication.DatabaseMonitor;
using KebabApplication.Services;
using KebabApplication.Services.Contracts;
using KebabApplication.StateMachine;
using KebabInfrastructure.Context;
using KebabInfrastructure.DatabaseMonitor;
using KebabInfrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using System.Configuration;
using System.Threading;
using System.Windows;

namespace KebabMasterApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {

            Logger? logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File(@"./", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            services.AddTransient<Logger>(x =>  logger);
            services.Configure<KebabDBConnectionSettings>(options => 
                options.ConnectionString = ConfigurationManager.ConnectionStrings["KebabDB"].ConnectionString);

            services.AddDbContext<KebabDbContext>();
            services.AddTransient<IMenuService, MenuService>();

            services.AddTransient<IOrderService, OrdersService>();
            services.AddTransient<IOrderStateMachine, OrderStatusSimpleStateMachine>();

            services.AddTransient<IDatabaseMonitor>(opt => 
                new DatabaseMonitor(SynchronizationContext.Current, 
                    serviceProvider.GetService<IOptions<KebabDBConnectionSettings>>(), 
                    serviceProvider.GetService<KebabDbContext>()));
            services.AddSingleton<MainWindow>();
        }
        
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
