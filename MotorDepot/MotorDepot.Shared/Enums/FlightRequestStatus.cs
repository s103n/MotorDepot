namespace MotorDepot.Shared.Enums
{
    /// <summary>
    /// Status of flight request, using for determine status of flight request
    /// in the program.
    /// After sending request for flight by driver,
    /// status of request automatically stayed in queue,
    /// that means that dispatcher or admin has to accept or cancel it
    /// </summary>
    public enum FlightRequestStatus
    {
        /// <summary>
        /// In queue status
        /// </summary>
        InQueue,
        /// <summary>
        /// Accepted status
        /// </summary>
        Accepted,
        /// <summary>
        /// Canceled status
        /// </summary>
        Canceled
    }
}
