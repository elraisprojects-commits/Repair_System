using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class ServiceRequest : BaseEntity
    {
        public string RequestNumber { get; set; } = null!;         //print on the device

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public int DeviceId { get; set; }
        public Device Device { get; set; } = null!;


        public string CustomerComplaint { get; set; } = null!;  //receptionist fill it out

        public string? InspectionResult { get; set; }           //Technical reason

        public string? ProblemCause { get; set; }              // reason from management

        public decimal? Cost { get; set; }                    //by management

        public int? ExpectedDays { get; set; }               //by management

        public string? RejectionReason { get; set; }           //by management 

        public RequestStatus Status { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; } = null!;


        public ICollection<RequestNote> Notes { get; set; } = new List<RequestNote>();   //by spcialisted


        public string CreatedById { get; set; } = null!;
        public ApplicationUser CreatedBy { get; set; } = null!;

        public string? SpecialistId { get; set; }
        public ApplicationUser? Specialist { get; set; }

        public DateTime? CustomerResponseDate { get; set; }

        public int RequestSequence { get; set; }

        public string? ReceptionistName { get; set; } = null!;




        public string? DeliveredById { get; set; }
        public ApplicationUser? DeliveredBy { get; set; }

        public string? DeliveredByName { get; set; }

        public DateTime? DeliveredAt { get; set; }

        public DateTime? CompletedAt { get; set; }




    }
}
