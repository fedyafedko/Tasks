using AutoMapper;
using Tasks.Common.DTOs;
using Tasks.Entities;

namespace Tasks.BLL.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterDTO, User>();
        }
    }
}
