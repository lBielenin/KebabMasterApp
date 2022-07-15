using KebabApplication.DatabaseMonitor;
using KebabApplication.Services.Contracts;
using KebabApplication.StateMachine;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using System;

namespace KebabMasterApp.ContentStrategy
{
    internal class DisplayStrategyContext : IDisplayStrategyContext
    {
        private readonly IServiceProvider serviceProvider;

        public DisplayStrategyContext(
            IServiceProvider provider)
        {
            serviceProvider = provider;
        }

        public IStrategy GetStrategyFromCommandArgs()
        {
            string[]? args = Environment.GetCommandLineArgs();

            if (args is null || args.Length == 0)
            {
                return new MenuStrategy();
            }  

            var lastArg = args[args.Length - 1];

            return GetStrategyBasedOnArgs(lastArg);
        }

        private IStrategy GetStrategyBasedOnArgs(string lastArg)
        {
            switch (lastArg)
            {
                case "-management":
                    return new OrderManagementStrategy(serviceProvider.GetService<IOrderService>(), serviceProvider.GetService<IDatabaseMonitor>(), serviceProvider.GetService<IOrderStateMachine>());
                case "-viewer":
                    return new OrderViewerStrategy(serviceProvider.GetService<IOrderService>(), serviceProvider.GetService<IDatabaseMonitor>());
                default:
                    return new MenuStrategy();
            }
        }
    }
}
