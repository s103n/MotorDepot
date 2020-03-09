using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MotorDepot.BLL.BusinessModels
{
    public static class ReflectionSort<TEntity> where TEntity : class
    {
        public static IEnumerable<TEntity> Sort(IEnumerable<TEntity> entities, string property, bool asc = true)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            var entityProps = new List<PropertyInfo>(typeof(TEntity).GetProperties());

            if (string.IsNullOrEmpty(property))
                throw new ArgumentException("Property cannot be empty");

            if (entityProps.All(x => x.GetType().GetProperty(property) == null))
                throw new ArgumentException("Property doesn't exist");

            if (asc)
                return entities.OrderBy(x => x.GetType().GetProperty(property).GetValue(x, null));
            else
                return entities.OrderByDescending(x => x.GetType().GetProperty(property).GetValue(x, null));
        }
    }
}
