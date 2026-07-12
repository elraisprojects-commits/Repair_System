using RepairCenter.Services.Invoices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Invoices
{
    public interface IInvoiceService
    {
        Task<InvoiceDto?> GetByIdAsync(int requestId);

        Task<InvoiceDto?> GetByRequestNumberAsync(string requestNumber);
    }
}
