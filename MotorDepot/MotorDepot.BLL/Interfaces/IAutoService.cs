using MotorDepot.BLL.Infrastructure;
using MotorDepot.BLL.Infrastructure.Enums;
using MotorDepot.BLL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MotorDepot.BLL.Interfaces
{
    public interface IAutoService : IDisposable
    {
        Task<OperationStatus> CreateAutoAsync(AutoDto autoDto);
        Task<OperationStatus> EditAutoAsync(AutoDto autoDto);
        Task<OperationStatus<IEnumerable<AutoDto>>> GetAutosByTypeAsync(AutoType type);
        Task<OperationStatus<IEnumerable<AutoBrandDto>>> GetBrandsAsync();
        OperationStatus<IEnumerable> GetAutoTypes();
    }
}
