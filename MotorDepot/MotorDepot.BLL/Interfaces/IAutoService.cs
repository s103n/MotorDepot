using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Interfaces
{
    public interface IAutoService : IDisposable
    {
        /// <summary>
        /// Creating new auto entity, possible be bad <c>http status code</c>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throwing if parameter is null</exception>
        /// <param name="autoDto"></param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> CreateAutoAsync(AutoDto autoDto);
        /// <summary>
        /// Updating auto entity, possible be bad <c>http status code</c>
        /// </summary>
        /// <exception cref="ArgumentNullException">Throwing if parameter is null</exception>
        /// <param name="autoDto"></param>
        /// <returns>Operation status</returns>
        Task<OperationStatus> EditAutoAsync(AutoDto autoDto);
        /// <summary>
        /// Getting IEnumerable object of auto dto objects by auto type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>IEnumerable objects</returns>
        Task<IEnumerable<AutoDto>> GetAutosByTypeAsync(AutoType type);
        /// <summary>
        /// Getting all auto brands 
        /// </summary>
        /// <returns>IEnumerable object of auto brand dto objects</returns>
        Task<IEnumerable<AutoBrandDto>> GetBrandsAsync();
        /// <summary>
        /// Getting all types of auto (IEnumerable object of anonymous object which contains 2 properties
        /// Id - int, Name - string)
        /// </summary>
        /// <remarks>(simple reflection of enum AutoTypes)</remarks>
        /// <returns>
        /// IEnumerable object
        /// </returns>
        IEnumerable GetAutoTypes();

        /// <summary>
        /// Getting all auto dto objects
        /// </summary>
        /// <returns>IEnumerable object of auto dto objects</returns>
        Task<IEnumerable<AutoDto>> GetAutosAsync(AutoStatus? status);
        /// <summary>
        /// Getting an auto object by id property
        /// </summary>
        /// <exception cref="ArgumentNullException">Throwing if parameter is null</exception>
        /// <param name="autoId">Id of auto</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus<AutoDto>> GetAutoById(int? autoId);
        /// <summary>
        /// Set status to auto
        /// </summary>
        /// <param name="status">Future status of auto</param>
        /// <param name="autoId">Id of auto</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> SetStatus(AutoStatus status, int autoId);

        /// <summary>
        /// Getting all auto statuses (IEnumerable object of anonymous object which contains 2 properties
        /// Id - int, Name - string)
        /// </summary>
        /// <returns>IEnumerable object</returns>
        IEnumerable GetAutoStatuses(bool deletedStatus = false);
    }
}
