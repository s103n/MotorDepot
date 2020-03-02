using System;

namespace MotorDepot.DAL.Entities.Abstract
{
    public abstract class BaseEnumEntity<T> where T : Enum
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }
}
