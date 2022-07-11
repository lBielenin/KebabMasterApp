using KebabApplication.DTO;
using KebabApplication.Services;
using KebabCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabApplication.StateMachine
{
    public class OrderStatusSimpleStateMachine
    {
        private OrdersService ordersService;

        public OrderStatusSimpleStateMachine(OrdersService service)
        {
            ordersService = service;
        }

        public void UpState(OrderDTO order, ICollection<OrderDTO> orders)
        {
            switch (order.Status)
            {
                case Status.Added:
                    order.Status = Status.InProgress;
                    break;
                case Status.InProgress:
                    order.Status = Status.Completed;
                    break;
                case Status.Completed:
                    orders.Remove(order);
                    RemoveOrder(order);
                    break;
            }
        }

        private void RemoveOrder(OrderDTO order)
        {
            ordersService.RemoveOrder(order.OrderId);
        }
    }
}
