using MotorDepot.BLL.Models;

namespace MotorDepot.BLL.Infrastructure
{
    public static class Extensions
    {
        /// <summary>
        /// Extension method for mapping one model to another with assignment all properties
        /// </summary>
        /// <param name="model">The model to which to assign</param>
        /// <param name="other">The model to be assigned</param>
        /// <returns></returns>
        public static FlightDto ToEditWith(this FlightDto model, FlightDto other)
        {
            model.Description = other.Description;
            model.Status = other.Status;
            model.ArrivalPlace = other.ArrivalPlace;
            model.DeparturePlace = other.DeparturePlace;
            model.Distance = other.Distance;

            return model;
        }
    }
}
