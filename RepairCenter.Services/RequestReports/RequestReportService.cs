using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.Services.RequestReports.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestReports
{
    public class RequestReportService : IRequestReportService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RequestReportService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RequestReportDto>> GetReportAsync(
            RequestReportFilterDto dto)
        {
            var query = _context.ServiceRequests

                .Include(x => x.Customer)

                .Include(x => x.Device)

                .Include(x => x.Branch)

                .Include(x => x.CreatedBy)

                .Include(x => x.Specialist)

                .AsQueryable();

            // ==========================
            // Request Number
            // ==========================

            if (!string.IsNullOrWhiteSpace(dto.RequestNumber))
            {
                query = query.Where(x =>
                    x.RequestNumber.Contains(dto.RequestNumber));
            }

            // ==========================
            // Customer Name
            // ==========================

            if (!string.IsNullOrWhiteSpace(dto.CustomerName))
            {
                query = query.Where(x =>
                    x.Customer.Name.Contains(dto.CustomerName));
            }

            // ==========================
            // Phone
            // ==========================

            if (!string.IsNullOrWhiteSpace(dto.Phone))
            {
                query = query.Where(x =>
                    x.Customer.PhoneNumber1.Contains(dto.Phone));
            }

            // ==========================
            // Branch
            // ==========================

            if (dto.BranchId.HasValue)
            {
                query = query.Where(x =>
                    x.BranchId == dto.BranchId);
            }

            // ==========================
            // Receptionist
            // ==========================

            if (!string.IsNullOrWhiteSpace(dto.ReceptionistId))
            {
                query = query.Where(x =>
                    x.CreatedById == dto.ReceptionistId);
            }

            // ==========================
            // Specialist
            // ==========================

            if (!string.IsNullOrWhiteSpace(dto.SpecialistId))
            {
                query = query.Where(x =>
                    x.SpecialistId == dto.SpecialistId);
            }

            // ==========================
            // Status
            // ==========================

            if (dto.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == dto.Status);
            }

            // ==========================
            // Cost
            // ==========================

            if (dto.MinCost.HasValue)
            {
                query = query.Where(x =>
                    x.Cost >= dto.MinCost.Value);
            }

            if (dto.MaxCost.HasValue)
            {
                query = query.Where(x =>
                    x.Cost <= dto.MaxCost.Value);
            }

            // ==========================
            // Date
            // ==========================

            if (dto.FromDate.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt.Date >= dto.FromDate.Value.Date);
            }

            if (dto.ToDate.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt.Date <= dto.ToDate.Value.Date);
            }

            var requests = await query

    .OrderByDescending(x => x.CreatedAt)

    .ToListAsync();

            var result = _mapper.Map<List<RequestReportDto>>(requests);

            foreach (var item in result)
            {
                var request = requests.First(x => x.Id == item.RequestId);

                item.CustomerName =
                    request.Customer.Name;

                item.Phone =
                    request.Customer.PhoneNumber1;

                item.Device =
                    $"{request.Device.Brand} {request.Device.Model}";

                item.Branch =
                    request.Branch.Name;

                item.Receptionist =
                    request.CreatedBy.FullName!;

                item.Specialist =
                    request.Specialist?.FullName;
            }

            return result;
        }
    }
}