using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepairCenter.data.Contexts;
using RepairCenter.data.Entities;
using RepairCenter.data.Enums;
using RepairCenter.Services.Notification;
using RepairCenter.Services.Requests.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Requests
{
    public class RequestService : IRequestService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public RequestService(
            AppDbContext context,
            IMapper mapper,
            INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<(int RequestId, string RequestNumber)> CreateRequestAsync(
    CreateRequestDto dto,
    string userId)
        {
            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                // Customer
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(x =>
                        x.PhoneNumber1 == dto.PhoneNumber1);

                if (customer == null)
                {
                    customer = _mapper.Map<Customer>(dto);

                    _context.Customers.Add(customer);

                    await _context.SaveChangesAsync();
                }

                // Device
                var device = _mapper.Map<Device>(dto);

                device.CustomerId = customer.Id;

                _context.Devices.Add(device);

                await _context.SaveChangesAsync();

                // Request Number
                var lastSequence = await _context.ServiceRequests
                    .MaxAsync(x => (int?)x.RequestSequence);

                var nextSequence = (lastSequence ?? 0) + 1;

                // Service Request
                var request = new ServiceRequest
                {
                    RequestSequence = nextSequence,

                    RequestNumber =
                        $"RQ-{DateTime.UtcNow.Year}-{nextSequence:D6}",

                    CustomerId = customer.Id,

                    DeviceId = device.Id,

                    CustomerComplaint = dto.CustomerComplaint,

                    Status = RequestStatus.Received,

                    ReceptionistName = dto.ReceptionistName,

                    CreatedById = userId,

                    BranchId = dto.BranchId
                };

                _context.ServiceRequests.Add(request);

                await _context.SaveChangesAsync();
                await _notificationService.CreateForAllEmployeesAsync(
                  NotificationType.RequestCreated,
                   request.Id,
                   request.RequestNumber);

                await transaction.CommitAsync();

                return (
                    request.Id,
                    request.RequestNumber
                );
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<List<RequestListDto>> GetAllAsync()
        {
            var requests = await _context.ServiceRequests
                .Include(x => x.Customer)
                .Include(x => x.Device)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<RequestListDto>>(requests);
        }

        public async Task<RequestDetailsDto?> GetByIdAsync(int id)
        {
            var request = await _context.ServiceRequests
                .Include(x => x.Customer)
                .Include(x => x.Device)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (request == null)
                return null;

            return _mapper.Map<RequestDetailsDto>(request);
        }


        public async Task<List<RequestListDto>> FilterAsync(
    RequestFilterDto filter)
        {
            var query = _context.ServiceRequests
                .Include(x => x.Customer)
                .Include(x => x.Device)
                .AsQueryable();

            if (filter.BranchId.HasValue)
            {
                query = query.Where(x =>
                    x.BranchId == filter.BranchId);
            }

            if (filter.Status.HasValue)
            {
                query = query.Where(x =>
                    x.Status == filter.Status);
            }

            if (!string.IsNullOrWhiteSpace(filter.RequestNumber))
            {
                query = query.Where(x =>
                    x.RequestNumber.Contains(filter.RequestNumber));
            }

            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                query = query.Where(x =>
                    x.Customer.PhoneNumber1.Contains(filter.PhoneNumber));
            }

            if (filter.FromDate.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt.Date >= filter.FromDate.Value.Date);
            }

            if (filter.ToDate.HasValue)
            {
                query = query.Where(x =>
                    x.CreatedAt.Date <= filter.ToDate.Value.Date);
            }

            var requests = await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<RequestListDto>>(requests);
        }
    }

}