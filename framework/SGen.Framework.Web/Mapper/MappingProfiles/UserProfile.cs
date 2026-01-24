using AutoMapper;


namespace SGen.Web.Infrastructure.Mapper.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Entity -> DTO
            //CreateMap<User, UserDto>();

            // DTO -> Entity (if needed)
            //CreateMap<UserDto, User>();
        }
    }
}
