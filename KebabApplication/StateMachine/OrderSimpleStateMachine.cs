using KebabApplication.DTO;
using KebabApplication.Services.Contracts;
using KebabCore.Enums;

namespace KebabApplication.StateMachine
{
    public class OrderStatusSimpleStateMachine : IOrderStateMachine
    {
        private IOrderService ordersService;

        public OrderStatusSimpleStateMachine(IOrderService service)
        {
            ordersService = service;
        }

        public void UpState(OrderDTO order, ICollection<OrderDTO> orders)
        {
            switch (order.Status)
            {
                case Status.Added:
                    order.Status = Status.InProgress;
                    UpdateStatus(order, Status.InProgress);
                    break;
                case Status.InProgress:
                    order.Status = Status.Completed;
                    UpdateStatus(order, Status.Completed);
                    break;
                case Status.Completed:
                    orders.Remove(order);
                    RemoveOrder(order);
                    break;
            }
        }

        private void UpdateStatus(OrderDTO order, Status status)
        {
            ordersService.UpdateOrderStatus(order.OrderId, status);
        }
        private void RemoveOrder(OrderDTO order)
        {
            ordersService.RemoveOrder(order.OrderId);
        }
    }
}
