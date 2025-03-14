using AutoMapper;
using BusinessLayer.Helper;
using SharedLibrary.DbModels.Request;
using SharedLibrary.DbModels.Response;
using SharedLibrary.RequestModels;
using SharedLibrary.ResponseModels;

namespace BusinessLayer.Mapping
{
	public class MappingProfile: Profile
	{
        public MappingProfile()
        {
			CreateMap<RegistrationRequest, UserEmailRequest>()
		   .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

			CreateMap<RegistrationRequest, User>()
			.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
			.ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
			.ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => (int)src.Country))
			//.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => PasswordHelper.HashAndStore(src.Password)))
			.ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));

			CreateMap<LoginRequest, LoginRequestDB>()
			.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
			.ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

			//CreateMap<CarViewResponse, CarViewResponseDB>()
			//.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
			//.ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
			//.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
			////.ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
			//.ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
			//.ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
			//.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
			//.ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand));

		}
    }
}
