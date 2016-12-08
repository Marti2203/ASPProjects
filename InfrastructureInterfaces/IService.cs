using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureInterfaces
{
    public interface IService<DTOType>
    {
        void Insert(DTOType entity);
        DTOType Get(int id);
        void Remove(int id);
        bool Has(int id);
        IQueryable<DTOType> EntitiesQuery();
    }
}
