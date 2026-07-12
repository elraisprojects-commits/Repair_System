using RepairCenter.Services.DeliverRequest.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.DeliverRequest
{
    public interface IDeliveryService
    {
        Task DeliverAsync(
            DeliverRequestDto dto,
            string userId);
    }
}
