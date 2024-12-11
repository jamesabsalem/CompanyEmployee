using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace CompanyEmployee;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<Company, CompanyDto>()
        //    .ForCtorParam("FullAddress",
        //        opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

        CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.FullAddress, 
                opt => opt.MapFrom(src => $"{src.Address} {src.Country}"));
        CreateMap<CompanyForCreationDto, Company>();
        CreateMap<CompanyForUpdateDto, Company>();

        CreateMap<Employee, EmployeeDto>();
        CreateMap<EmployeeForCreationDto, Employee>();
        CreateMap<EmployeeForUpdateDto, Employee>();

    }
}