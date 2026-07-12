using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.data.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string Area { get; set; }

        [Phone]
        public string PhoneNumber1 { get; set; }

        public string? PhoneNumber2 { get; set; }


        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    = new List<ServiceRequest>();
    }

}
