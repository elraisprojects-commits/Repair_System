using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class RequestNote : BaseEntity
    {
        public int ServiceRequestId { get; set; }
        public ServiceRequest ServiceRequest { get; set; } = null!;

        public string? Note { get; set; } = null!;

        public RequestStatus Status { get; set; }

        public string? CreatedById { get; set; } = null!;
        public ApplicationUser? CreatedBy { get; set; } = null!;

        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public DateTime? CancelledAt { get; set; }
    }
}
