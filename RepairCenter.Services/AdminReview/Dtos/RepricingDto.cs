using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.AdminReview.Dtos
{
    public class RepricingDto
    {
        public int RequestId { get; set; }

        public decimal Cost { get; set; }

        public string? Note { get; set; }

        public RequestStatus Status { get; set; }
    }
}
