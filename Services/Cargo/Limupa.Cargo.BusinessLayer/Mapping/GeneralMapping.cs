using AutoMapper;
using Limupa.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using Limupa.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using Limupa.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using Limupa.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.BusinessLayer.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            // CargoCompany Mapping

            CreateMap<CargoCompany,ResultCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany,CreateCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany,UpdateCargoCompanyDto>().ReverseMap();
            CreateMap<CargoCompany,GetCargoCompanyByIdDto>().ReverseMap();

            // CargoCustomer Mapping

            CreateMap<CargoCustomer, ResultCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer,CreateCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer,UpdateCargoCustomerDto>().ReverseMap();
            CreateMap<CargoCustomer,GetCargoCustomerByIdDto>().ReverseMap();

            // CargoDetail Mapping

            CreateMap<CargoDetail,ResultCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail,CreateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail,UpdateCargoDetailDto>().ReverseMap();
            CreateMap<CargoDetail,GetCargoDetailByIdDto>().ReverseMap();

            // CargoOperation Mapping

            CreateMap<CargoOperation,ResultCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation,CreateCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation,UpdateCargoOperationDto>().ReverseMap();
            CreateMap<CargoOperation,GetCargoOperationByIdDto>().ReverseMap();
        }
    }
}
