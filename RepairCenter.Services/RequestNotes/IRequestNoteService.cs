using RepairCenter.Services.RequestNotes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestNotes
{
    public interface IRequestNoteService
    {
        Task AddNoteAsync(
            AddRequestNoteDto dto,
            string userId);
    }
}
