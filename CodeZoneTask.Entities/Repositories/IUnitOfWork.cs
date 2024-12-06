using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneTask.Entities.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IStoreRepository Store { get; }
        IItemRepository Item { get; }
        int Complete();
    }
}
