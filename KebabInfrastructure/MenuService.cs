using KebabCore.Entities.Menu;
using KebabInfrastructure.Context;
using System;
using System.Collections.Generic;
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

        public List<MenuItem> GetNewest()
        {
            List<MenuItem> items;

            Guid menuId = DbContext.Menus.OrderByDescending(m => m.CreationDate).First().MenuId;
            items = DbContext.MenuItems.Where(i => i.MenuId == menuId).ToList();

            
            return items;
        }


        public void Dispose()
        {
            dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
