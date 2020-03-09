using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IDriverService : IDisposable
    {
        /// <summary>
        /// Creating new driver
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="driver">User dto object</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> CreateDriver(UserDto driver);
        /// <summary>
        /// Getting all drivers
        /// </summary>
        /// <returns>IEnumerable of user dto objects where role is driver</returns>
        IEnumerable<UserDto> GetDrivers();
        /// <summary>
        /// Creating new flight request object for flight by current driver
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="flightRequest">Flight request object</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> SendFlightRequest(FlightRequestDto flightRequest);
        /// <summary>
        /// Getting driver by id property
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="id">Id of driver</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus<UserDto>> GetDriverById(string id);
        /// <summary>
        /// Getting flights by driver id
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="driverId">Id of driver</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus<IEnumerable<FlightDto>>> GetFlightsByDriver(string driverId);
        /// <summary>
        /// Getting current flight of concrete driver
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="driverId">Id of driver</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus<FlightDto>> CurrentFlight(string driverId);
    }
}
