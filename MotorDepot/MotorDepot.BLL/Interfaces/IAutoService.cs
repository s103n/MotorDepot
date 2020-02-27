using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Models;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IAutoService
    {
        Task<OperationStatus> CreateAuto(AutoDto autoDto);
        Task EditAuto(AutoDto autoDto);
    }
}
