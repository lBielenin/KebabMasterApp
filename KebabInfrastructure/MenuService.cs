using KebabCore.DomainModels.Menu;
using KebabCore.Views;
using KebabInfrastructure.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabInfrastructure
{
    public class MenuService : IDisposable
    {
        private KebabMenuDbContext dbContext;
        private KebabMenuDbContext DbContext
        {
            get
            {
                if (dbContext is null)
                    dbContext = new KebabMenuDbContext();
                return dbContext;
            }
        }

        public List<MenuView> GetNewestMenu()
        {
            using (var context = new KebabMenuDbContext())
            {
                var id = GetNewestMenuId(context);
                return context.MenuView.Where(v => v.MenuId == id).ToList();

            }
        }

        private Guid GetNewestMenuId(KebabMenuDbContext context)
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
