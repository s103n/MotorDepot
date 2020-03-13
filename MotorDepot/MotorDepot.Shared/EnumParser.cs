using System;
using System.Collections;
using MotorDepot.Shared.Interfaces;

namespace MotorDepot.Shared
{
    public class EnumParser<T> : IEnumParser<T> where T : Enum
    {
        public IEnumerable Parse()
        {
            foreach (var item in Enum.GetNames(typeof(T)))
            {
                yield return new
                {
                    Name = item,
                    Id = (int)Enum.Parse(typeof(T), item)
                };
            }
        }
    }
}
