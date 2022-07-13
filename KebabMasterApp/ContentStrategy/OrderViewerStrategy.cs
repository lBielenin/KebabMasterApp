using KebabApplication.DatabaseMonitor;
using KebabApplication.Services.Contracts;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Windows.Controls;

namespace KebabMasterApp.ContentStrategy
{
    internal class OrderViewerStrategy : IStrategy
    {
        private readonly IOrderService orderService;
        private readonly IDatabaseMonitor databaseMonitor;
        private readonly Logger logger;

        public OrderViewerStrategy(
            IOrderService orderService, 
            IDatabaseMonitor databaseMonitor,
            Logger logger)
        {
            this.orderService = orderService;
            this.databaseMonitor = databaseMonitor;
            this.logger = logger;
        }
        public void DisplayContent(ContentControl contentRef)
        {
            contentRef.Content = new OrderViewerControl(orderService, databaseMonitor, logger);
        }
    }
}
