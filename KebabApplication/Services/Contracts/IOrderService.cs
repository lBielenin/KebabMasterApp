using KebabApplication.DTO;
using KebabCore.DomainModels.Orders;
using KebabCore.Enums;

namespace KebabApplication.Services.Contracts
{
    public interface IOrderService
    {
        List<OrderDTO> GetTodayOrders();
        int PlaceOrderReturnValue(Order order);
        void UpdateOrderStatus(Guid orderId, Status status);
        void RemoveOrder(Guid orderId);
        
    }
}
