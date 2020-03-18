using MotorDepot.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.Shared.Enums;

namespace MotorDepot.BLL.Interfaces
{
    public interface IFlightService : IDisposable
    {
        /// <summary>
        /// Getting all flights
        /// </summary>
        /// <exception cref="ArgumentException">Can not use delete and only free as true values together</exception>
        /// <returns>IEnumerable object of flights</returns>
        Task<IEnumerable<FlightDto>> GetFlightsAsync(FlightStatus? status);
        /// <summary>
        /// Creating new flight
        /// </summary>
        /// <param name="flightDto">Flight dto object</param>
        /// <exception cref="ArgumentNullException">Throwing if argument is null</exception>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> CreateFlightAsync(FlightDto flightDto);
        /// <summary>
        /// Removing flight (setting status to deleted)
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="id">Id of flight</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> RemoveFlightAsync(int? id);
        /// <summary>
        /// Updating flight
        /// </summary>
        /// <exception cref="ArgumentNullException">Throwing if argument is null</exception>
        /// <param name="flightDto">Flight dto object</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> EditFlightAsync(FlightDto flightDto);
        /// <summary>
        /// Getting flight by id property
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="id">Id of flight</param>
        /// <returns>Flight dto object</returns>
        Task<OperationStatus<FlightDto>> GetFlightAsync(int? id);
        /// <summary>
        /// Setting status for flight
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="status">Status of flight</param>
        /// <param name="flightId">Id of flight</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> SetStatus(FlightStatus status, int? flightId);
        /// <summary>
        /// Setting driver and auto to flight
        /// </summary>
        /// <param name="flightId">Id of flight</param>
        /// <param name="autoId">Id of auto</param>
        /// <param name="driverId">Id of driver</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> SetDriverWithAuto(int flightId, int? autoId, string driverId);
        /// <summary>
        /// Getting all flight statuses (IEnumerable object of anonymous object which contains
        /// 2 properties Name - string and Id - int)
        /// </summary>
        /// <returns>IEnumerable object</returns>
        OperationStatus<IEnumerable> GetFlightStatuses();
        /// <summary>
        /// Deleting driver and auto from flight
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="flightId">Id of flight</param>
        /// <returns>Result of operation</returns>
        Task<OperationStatus> DeleteDriverAndAuto(int? flightId);
    }
}
