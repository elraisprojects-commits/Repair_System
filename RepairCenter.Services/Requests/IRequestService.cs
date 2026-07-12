using RepairCenter.Services.Requests.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Requests
{
    public interface IRequestService
    {
        Task<(int RequestId, string RequestNumber)> CreateRequestAsync(
            CreateRequestDto dto,
            string userId);

        Task<List<RequestListDto>> GetAllAsync();

        Task<RequestDetailsDto?> GetByIdAsync(int id);

        Task<List<RequestListDto>> FilterAsync(
        RequestFilterDto filter);


    }
}
