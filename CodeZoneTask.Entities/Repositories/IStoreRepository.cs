using CodeZoneTask.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneTask.Entities.Repositories
{
    public interface IStoreRepository:IGenericRepository<Store>
    {
        void Update(Store store);
    }
}
