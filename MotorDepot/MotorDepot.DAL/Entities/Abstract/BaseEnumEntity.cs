using System;

namespace MotorDepot.DAL.Entities.Abstract
{
    public abstract class BaseEnumEntity<TEnum>
        where TEnum : Enum
    {
        public TEnum Id { get; set; }
        public string Name { get; set; }
    }
}
