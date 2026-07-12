using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepairCenter.Services.Invoices;

namespace RepairCenter.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(
            IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        //  Id السيرش بالـ 

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        //RequestNumber السيرش بال 

        [HttpGet("number/{requestNumber}")]
        public async Task<IActionResult> GetByRequestNumber(string requestNumber)
        {
            var invoice =
                await _invoiceService.GetByRequestNumberAsync(requestNumber);

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }
    }
}