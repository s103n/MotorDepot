using System.Threading.Tasks;
using MotorDepot.BLL.Models;

namespace MotorDepot.BLL.Interfaces
{
    public interface ILoggerService
    {
        Task Log(ExceptionDto exception);
    }
}
