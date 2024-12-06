using CodeZoneTask.Entities.Models;
using CodeZoneTask.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneTask.DataAccess.Implementation
{
    public class ItemRepository : GenericRepository<Item>,IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Item item)
        {
            var ItemInDb = _context.Items.Find(item.Id);
            if (ItemInDb != null)
            {
                _context.Entry(ItemInDb).CurrentValues.SetValues(item);
                _context.SaveChanges();
            }
        }
    }
}
