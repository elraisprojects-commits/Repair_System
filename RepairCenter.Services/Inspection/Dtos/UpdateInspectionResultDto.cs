using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inspection.Dtos
{
    public class UpdateInspectionResultDto
    {
        public int RequestId { get; set; }

        public string InspectionResult { get; set; } = null!;
    }
}

