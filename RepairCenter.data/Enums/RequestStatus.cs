using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Enums
{
    public enum RequestStatus
    {
        
    
        Received,                                  //تم الاستلام 
        UnderReview,                              //تحت الفحص 
        CompanyRejected,                         //مرفوض من الشركه 
        WaitingCustomerApproval,                //في اننظار الرد من العميل 
        InProgress,                            //جاري الاصلاح 
        Completed,                            //تم الانتهاء     
        Delivered,                           //تم الاستلام 
        CancelledByCustomer                 //تم الرفض من العميل 
    }
}

