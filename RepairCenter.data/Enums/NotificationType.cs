using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Enums
{
    public enum NotificationType
    {
        RequestCreated,

        InspectionSubmitted,

        WaitingCustomerApproval,

        WaitingCustomerResponse,

        CustomerApproved,

        CustomerRejected,

        RequestRejectedByAdmin,

        RequestUpdated,

        RequestCompleted,

        RequestDelivered

        
    }
}