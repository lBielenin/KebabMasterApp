using KebabApplication.Services.Contracts;
using KebabInfrastructure.Context;
using KebabInfrastructure.Views;

namespace KebabApplication.Services
{
    public class MenuService : IDisposable, IMenuService
    {
        private KebabDbContext dbContext;

        public MenuService(KebabDbContext context)
        {
            dbContext = context;
        }

        public List<MenuView> GetNewestMenu()
        {

            var id = GetNewestMenuId(dbContext);
            var menu = dbContext.MenuView.Where(v => v.MenuId == id).ToList();
            menu.Sort((MenuView vp, MenuView vn) => ((int)vp.ItemCategory).CompareTo((int)vn.ItemCategory));
            return menu;



        }

        private Guid GetNewestMenuId(KebabDbContext context)
        {
            return context.Menus
                .OrderByDescending(m => m.CreationDate).First().MenuId;
        }

        public void Dispose()
        {
            dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
