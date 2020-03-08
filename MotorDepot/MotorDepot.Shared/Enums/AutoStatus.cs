namespace MotorDepot.Shared.Enums
{
    /// <summary>
    ///Auto status can be changed by driver after the successful flight or by admin 
    /// </summary>
    public enum AutoStatus
    {
        /// <summary>
        /// Can be using in flights
        /// </summary>
        Usable, 
        /// <summary>
        /// Cannot be using in flights but can be fixed to use
        /// </summary>
        NeedFix,
        /// <summary>
        /// Cannot be using in flight anymore and waits on stock for deleting by admin 
        /// </summary>
        Unusable, 
        /// <summary>
        /// Deleted cannot be displayed or use for flights using
        /// </summary>
        Deleted,
    }
}
