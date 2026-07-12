using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestReports.Dtos
{
    public class RequestReportFilterDto
    {
        public string? RequestNumber { get; set; }

        public string? CustomerName { get; set; }

        public string? Phone { get; set; }

        public int? BranchId { get; set; }

        public string? ReceptionistId { get; set; }

        public string? SpecialistId { get; set; }

        public RequestStatus? Status { get; set; }

        public decimal? MinCost { get; set; }

        public decimal? MaxCost { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}