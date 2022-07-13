using KebabInfrastructure.Views;

namespace KebabApplication.Services.Contracts
{
    public interface IMenuService
    {
        List<MenuView> GetNewestMenu();
    }
}
