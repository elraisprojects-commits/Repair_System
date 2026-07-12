using RepairCenter.Services.Inspection.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inspection
{
    public interface IInspectionService
    {
        Task UpdateInspectionResultAsync(UpdateInspectionResultDto dto);
    }
}
