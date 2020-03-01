using System;
using System.ComponentModel.DataAnnotations;

namespace MotorDepot.DAL.Entities.Abstract
{
    public abstract class BaseEnumEntity<T> where T : Enum
    {
        public T Id { get; set; }

        [Required, StringLength(32, MinimumLength = 2)]
        public string Name { get; set; }
    }
}
