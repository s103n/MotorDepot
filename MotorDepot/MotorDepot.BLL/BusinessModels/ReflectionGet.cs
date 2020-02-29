using MotorDepot.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MotorDepot.BLL.BusinessModels
{
    public class ReflectionGet<T> where T : class
    {
        private readonly Type _type;
        private readonly object _property;

        public ReflectionGet(object property)
        {
            _type = typeof(T);
            _property = property;
        }

        private Type ObjectType => _property.GetType();
        private IEnumerable<PropertyInfo> ObjectProperties => new List<PropertyInfo>(ObjectType.GetProperties());
        private IEnumerable<PropertyInfo> EntityProperties => new List<PropertyInfo>(_type.GetProperties());

        public async Task<T> GetItem(IRepository<T> repository)
        {
            foreach (var prop in ObjectProperties)
            {
                if (EntityProperties.Any(p => p.Name == prop.Name))
                {
                    var item = await repository.FindAsync(p =>
                        _type.GetProperty(prop.Name, BindingFlags.Public)?.GetValue(p, null)
                        ==
                        prop.GetValue(ObjectType, null)
                    );

                    if (item == null) continue;

                    return item;
                }
            }

            return null;
        }
    }
}
