using AutoMapper;
using RepairCenter.data.Entities;
using RepairCenter.Services.Inventory.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairCenter.Services.Inventory
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile()
        {
            // Create
            CreateMap<CreateInventoryItemDto, InventoryItem>()
        .ForMember(dest => dest.ItemCode,
            opt => opt.Ignore())

        .ForMember(dest => dest.ItemSequence,
            opt => opt.Ignore())

        .ForMember(dest => dest.IsActive,
            opt => opt.Ignore())

        .ForMember(dest => dest.TotalPrice,
            opt => opt.MapFrom(src =>
                src.Quantity * src.UnitPrice))

        .ForMember(dest => dest.Id,
            opt => opt.Ignore())

        .ForMember(dest => dest.CreatedAt,
            opt => opt.Ignore());


            // Update
            CreateMap<UpdateInventoryItemDto, InventoryItem>()
                     .ForMember(dest => dest.ItemCode,
                           opt => opt.Ignore())

                    .ForMember(dest => dest.ItemSequence,
                            opt => opt.Ignore())

                   .ForMember(dest => dest.Quantity,
                            opt => opt.Ignore())

                   .ForMember(dest => dest.TotalPrice,
                            opt => opt.Ignore())

                  .ForMember(dest => dest.IsActive,
                             opt => opt.Ignore())

                  .ForMember(dest => dest.CreatedAt,
                            opt => opt.Ignore());



            // Details
            CreateMap<InventoryItem, InventoryItemDto>();



            // List
            CreateMap<InventoryItem, InventoryListDto>();
        }
    }
}