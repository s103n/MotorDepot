using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Interfaces;
using MotorDepot.BLL.Models;

namespace MotorDepot.BLL.Services
{
    public class AutoService : IAutoService
    {
        public Task<OperationStatus> CreateAuto(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }

        public Task EditAuto(AutoDto autoDto)
        {
            throw new NotImplementedException();
        }
    }
}
