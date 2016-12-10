using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureInterfaces
{
    internal class ServiceFactory
    {
        public static IService<DTOType> CreateService<DTOType,EntityType>() where DTOType : class, new()
        {
            Service<DTOType,EntityType> service = new Service<DTOType,EntityType> { Context = new ATPEntities() };
            return service;
        }

        internal class Service<DTOType, EntityType> : IService<DTOType> where DTOType : class, new()
        {
            public DbContext Context { get; internal set; }

            public IQueryable<DTOType> Entities()
            {
                List<DTOType> type = new List<DTOType>();
                foreach (object element in Context.Set(typeof(EntityType)).AsQueryable())
                {
                    type.Add(ConvertToDTO(element));
                }
                return type.AsQueryable();
            }

            public void Insert(DTOType element)
            {
                Context.Set(typeof(EntityType)).Add(element);
            }

            public DTOType Get(int id)
            {
                return ConvertToDTO(Context.Set(typeof(EntityType)).Find(id));
            }

            public bool Has(int id)
            {
                return Context.Set(typeof(EntityType)).Find(id) != null;
            }

            public void Remove(int id)
            {
                Context.Set(typeof(EntityType)).Remove(Context.Set(typeof(EntityType)).Find(id));
            }

            public int SaveChanges()
            {
                return Context.SaveChanges();
            }

            public DTOType ConvertToDTO(object element)
            {
                DTOType dto = new DTOType();
                PropertyInfo[] properties = element.GetType().GetProperties();
                for (int i = 0; i < properties.Length; i++)
                {
                    dto.GetType().GetProperties()[i].SetValue(dto, properties[i].GetValue(element));
                }
                return dto;
            }
        }
    }


}
