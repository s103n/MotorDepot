using System;
using System.Collections;

namespace MotorDepot.Shared.Interfaces
{
    /// <summary>
    /// Represents parser that parse any enum into IEnumerable object of anonymous type
    /// </summary>
    /// <typeparam name="T">Enum type</typeparam>
    public interface IEnumParser<T> where T : Enum
    {
        /// <summary>
        /// Returns IEnumerable object of anonymous type with 2 properties Name - string and Id - int
        /// </summary>
        /// <returns></returns>
        IEnumerable Parse();
    }
}
