using KebabApplication.DatabaseMonitor;
using KebabApplication.Services.Contracts;
using KebabApplication.StateMachine;
using Serilog.Core;
using System.Windows.Controls;

namespace KebabMasterApp.ContentStrategy
{
    internal class OrderManagementStrategy : IStrategy
    {
        private readonly IOrderService orderService;
        private readonly IDatabaseMonitor dbMonitor;
        private readonly IOrderStateMachine orderStateMachine;

        public OrderManagementStrategy(
            IOrderService ordService,
            IDatabaseMonitor monitor,
            IOrderStateMachine stateMachine)
        {
            orderService = ordService;
            dbMonitor = monitor;
            orderStateMachine = stateMachine;
        }
        public void DisplayContent(ContentControl contentRef)
        {
            contentRef.Content = new OrderManagementControl(orderService, dbMonitor, orderStateMachine);
        }
    }
}
