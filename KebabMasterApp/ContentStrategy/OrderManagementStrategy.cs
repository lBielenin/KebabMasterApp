using KebabApplication.DatabaseMonitor;
using KebabApplication.Services.Contracts;
using Serilog.Core;
using System.Windows.Controls;

namespace KebabMasterApp.ContentStrategy
{
    internal class OrderManagementStrategy : IStrategy
    {
        private readonly IOrderService orderService;
        private readonly IDatabaseMonitor monitor;
        private readonly Logger logger;

        public OrderManagementStrategy(
            IOrderService orderService,
            IDatabaseMonitor monitor,
            Logger logger)
        {
            this.orderService = orderService;
            this.monitor = monitor;
            this.logger = logger;
        }
        public void DisplayContent(ContentControl contentRef)
        {
            contentRef.Content = new OrderManagementControl(orderService, monitor, logger);
        }
    }
}
