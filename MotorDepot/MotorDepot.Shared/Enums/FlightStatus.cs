namespace MotorDepot.Shared.Enums
{
    /// <summary>
    /// Flight status define the concrete flight status
    /// </summary>
    public enum FlightStatus
    {
        /// <summary>
        /// Free for occupation by the driver
        /// </summary>
        Free,
        /// <summary>
        /// Occupied by driver
        /// </summary>
        Occupied,
        /// <summary>
        /// Performed by driver
        /// </summary>
        Performed,
        /// <summary>
        /// Completed by driver
        /// </summary>
        Completed,
        /// <summary>
        /// Cannot be displayed
        /// </summary>
        Deleted
    }
}
