using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.AdminReview.Dtos

{

    public class CompanyRejectDto
    {
        public int RequestId { get; set; }

        public string? RejectionReason { get; set; }
    }
}