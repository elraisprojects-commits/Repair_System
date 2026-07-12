using AutoMapper;
using RepairCenter.data.Entities;
using RepairCenter.Services.RequestNotes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.RequestNotes
{
    public class RequestNoteProfile : Profile
    {
        
            public RequestNoteProfile()
            {
                CreateMap<AddRequestNoteDto, RequestNote>()
                    .ForMember(x => x.ServiceRequestId,
                        opt => opt.MapFrom(src => src.RequestId))

                    .ForMember(x => x.Note,
                        opt => opt.MapFrom(src => src.Note))

                    .ForMember(x => x.Status,
                        opt => opt.MapFrom(src => src.Status))

                    .ForMember(x => x.CreatedById,
                        opt => opt.Ignore())

                    .ForMember(x => x.CreatedBy,
                        opt => opt.Ignore())

                    .ForMember(x => x.ServiceRequest,
                        opt => opt.Ignore())

                    .ForMember(x => x.Id,
                        opt => opt.Ignore())

                    .ForMember(x => x.CreatedAt,
                        opt => opt.Ignore());
            }
        }
  }
