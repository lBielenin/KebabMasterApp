using KebabCore.DomainModels.Menu;
using KebabInfrastructure.Context;
using KebabInfrastructure.Views;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KebabApplication.Services
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
