using AutoMapper;
using PharmacyOrderSystem.Application.DTOs;
using PharmacyOrderSystem.Domain.Entities;

namespace PharmacyOrderSystem.Application.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Customer Mapping
        CreateMap<Customer, CustomerDTO>().ReverseMap();

        // Medicine Mapping
        CreateMap<Medicine, MedicineDTO>().ReverseMap();

        // OrderItem Mapping
        CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name))
            .ReverseMap();

        // Order Mapping
        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
            .ReverseMap();
    }
}
