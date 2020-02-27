using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IDriverService
    {
        Task<OperationStatus> CreateDriver(UserDto userDto);
        IEnumerable<UserDto> GetDrivers();
    }
}
