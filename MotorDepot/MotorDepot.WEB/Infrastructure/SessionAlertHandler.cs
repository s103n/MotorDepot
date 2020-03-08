using System.Collections.Generic;

namespace MotorDepot.WEB.Infrastructure
{
    public static class SessionAlertHandler
    {
        private static readonly List<string> PossibleSessionAlertsValues = new List<string>
        {
            "Create",
            "Edit",
            "Update",
            "FlightStatus",
            "RequestForFlight",
            "Authenticate",
            "Delete"
        };

        public static bool ContainsInAlertKeys(this string sessionKey)
        {
            return PossibleSessionAlertsValues.Contains(sessionKey);
        }
    }
}