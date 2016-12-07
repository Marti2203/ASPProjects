using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureInterfaces
{
    public interface IService<Entity>
    {
        Entity Get(int id);
        void Remove(int id);
        bool Has(int id);
        IQueryable<Entity> EntitiesQuery { get; }
    }
}
