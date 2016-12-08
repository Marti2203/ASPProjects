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
    public class ServiceFactory
    {
        public static IService<DTOType> CreateService<DTOType,EntityType>() where DTOType : class, new()
        {
            Service<DTOType> service = new Service<DTOType> { Context = new ATPEntities() };
            string element = typeof(DTOType).Name;
            Type type = Assembly.LoadFile("./~/").GetType(element.Remove(element.Length - 3)); //Type.GetType(typeof(DTOType).Name.Remove(typeof(DTOType).Name.Length - 3), true);

            service.EntityType = typeof(EntityType);
            return service;
        }
    }
    internal class Service<DTOType>:IService<DTOType> where DTOType: class, new()
    {
        internal Type EntityType { get; set; }

        public DbContext Context { get; internal set; }

        public IQueryable<DTOType> EntitiesQuery()
        {
            List<DTOType> type = new List<DTOType>();
            foreach(object element in Context.Set(EntityType).AsQueryable())
            {
                type.Add(ConvertToDTO(element));
            }
            return type.AsQueryable();
        }

        public void Insert(DTOType element)
        {
            Context.Set(EntityType).Add(element);
        }

        public DTOType Get(int id)
        {
            return ConvertToDTO(Context.Set(EntityType).Find(id));
        }

        public bool Has(int id)
        {
            return Context.Set(EntityType).Find(id) != null;
        }

        public void Remove(int id)
        {
            Context.Set(EntityType).Remove(Context.Set(EntityType).Find(id));
            Context.SaveChanges();
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
