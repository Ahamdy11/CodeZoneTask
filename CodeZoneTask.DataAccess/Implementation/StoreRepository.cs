using CodeZoneTask.Entities.Models;
using CodeZoneTask.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneTask.DataAccess.Implementation
{
    public class StoreRepository : GenericRepository<Store>,IStoreRepository
    {
        private readonly ApplicationDbContext _context;
        public StoreRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Store store)
        {
            var StoreInDb = _context.Stores.Find(store.Id);
            if (StoreInDb != null)
            {
                _context.Entry(StoreInDb).CurrentValues.SetValues(store);
                _context.SaveChanges();
            }
        }
    }
}
