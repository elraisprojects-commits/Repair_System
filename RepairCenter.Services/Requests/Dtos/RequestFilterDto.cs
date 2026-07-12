using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Requests.Dtos
{
    public class RequestFilterDto
    {
        public int? BranchId { get; set; }

        public RequestStatus? Status { get; set; }

        public string? RequestNumber { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}

