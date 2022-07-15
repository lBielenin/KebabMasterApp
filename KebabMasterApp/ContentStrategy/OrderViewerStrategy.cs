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

        public OrderViewerStrategy(
            IOrderService ordService, 
            IDatabaseMonitor monitor)
        {
            orderService = ordService;
            databaseMonitor = monitor;
        }
        public void DisplayContent(ContentControl contentRef)
        {
            contentRef.Content = new OrderViewerControl(orderService, databaseMonitor);
        }
    }
}
