using KebabApplication.DTO;

namespace KebabApplication.StateMachine
{
    public interface IOrderStateMachine
    {
        public void UpState(OrderDTO order, ICollection<OrderDTO> orders);
    }
}
