using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureInterfaces
{
    public class ServiceFactory
    {
        public static IService<T> CreateService<T>() where T : class, new()
        {
            return new Service<T> { Context = new ATPEntities() };
        }
    }
    internal class Service<T>:IService<T> where T: class, new()
    {
        public DbContext Context { get; internal set; }

        public IQueryable<T> EntitiesQuery
        {
            get
            {
                return Context.Set(typeof(T)).AsQueryable() as IQueryable<T>;
            }
        }

        public T Get(int id)
        {
            return Context.Set(typeof(T)).Find(id) as T;
        }

        public bool Has(int id)
        {
            return Context.Set(typeof(T)).Find(id) != null;
        }

        public void Remove(int id)
        {
            Context.Set(typeof(T)).Remove(Context.Set(typeof(T)).Find(id));
        }
    }
}
