using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.AdminReview.Dtos
{
    public class AdminReviewDto
    {
        public int RequestId { get; set; }

        public string ProblemCause { get; set; } = null!;

        public decimal Cost { get; set; }

        public int ExpectedDays { get; set; }

        public RequestStatus Status { get; set; }

        public string? Note { get; set; }
    }
}
