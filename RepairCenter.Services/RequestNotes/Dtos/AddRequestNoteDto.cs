using RepairCenter.data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestNotes.Dtos
{
    public class AddRequestNoteDto
    {
        public int RequestId { get; set; }

        public string Note { get; set; } = null!;

        public RequestStatus? Status { get; set; }
    }
}
