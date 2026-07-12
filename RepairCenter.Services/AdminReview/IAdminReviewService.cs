using RepairCenter.Services.AdminReview.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.AdminReview
{
    public interface IAdminReviewService
    {
        Task AdminReviewAsync(AdminReviewDto dto, string userId);
       
        Task RejectByCompanyAsync(CompanyRejectDto dto);
    }
}

