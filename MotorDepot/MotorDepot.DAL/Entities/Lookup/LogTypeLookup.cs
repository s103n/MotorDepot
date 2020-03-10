using MotorDepot.DAL.Entities.Abstract;
using MotorDepot.DAL.Entities.Logging;
using MotorDepot.Shared.Enums;
using System.Collections.Generic;

namespace MotorDepot.DAL.Entities.Lookup
{
    public class LogTypeLookup : BaseEnumEntity<LogType>
    {
        public virtual ICollection<LogEvent> LogEvents { get; set; }
    }
}
