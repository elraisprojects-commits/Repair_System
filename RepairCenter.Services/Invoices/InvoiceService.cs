using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Enums;
using RepairCenter.Services.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceService(
            AppDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoiceDto?> GetByIdAsync(int requestId)
        {
            var request = await _context.ServiceRequests
                .Include(x => x.Customer)
                .Include(x => x.Device)
                .Include(x => x.Branch)
                .Include(x => x.Specialist)
                .FirstOrDefaultAsync(x => x.Id == requestId);

            if (request == null)
                return null;

            if (request.Status != RequestStatus.Completed &&
                request.Status != RequestStatus.Delivered)
            {
                throw new Exception("Invoice is available only after repair completion.");
            }

            return _mapper.Map<InvoiceDto>(request);
        }

        public async Task<InvoiceDto?> GetByRequestNumberAsync(string requestNumber)
        {
            var request = await _context.ServiceRequests
                .Include(x => x.Customer)
                .Include(x => x.Device)
                .Include(x => x.Branch)
                .Include(x => x.Specialist)
                .FirstOrDefaultAsync(x => x.RequestNumber == requestNumber);

            if (request == null)
                return null;

            if (request.Status != RequestStatus.Completed &&
                request.Status != RequestStatus.Delivered)
            {
                throw new Exception("Invoice is available only after repair completion.");
            }

            return _mapper.Map<InvoiceDto>(request);
        }
    }
}