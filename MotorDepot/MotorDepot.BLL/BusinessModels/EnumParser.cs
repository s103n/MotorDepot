using System;
using System.Collections;

namespace MotorDepot.BLL.BusinessModels
{
    public class EnumParser<T> where T : Enum
    {
        /// <summary>
        /// Returns IEnumerable object of enum type. IEnumerable contains anonymous object
        /// with 2 properties: int Id and string Name.
        /// </summary>
        /// <returns></returns>
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
