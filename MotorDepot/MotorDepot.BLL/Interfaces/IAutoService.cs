using System;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IAutoService : IDisposable
    {
        Task<OperationStatus> CreateAuto(AutoDto autoDto);
        Task EditAuto(AutoDto autoDto);
    }
}
