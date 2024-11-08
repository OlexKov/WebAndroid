using AutoMapper;
using WebAndroid.Data.Entities;
using WebAndroid.Models;

namespace WebAndroid.Mappers
{
    public class EntityUserProfile:Profile
    {
        public EntityUserProfile() 
        {
            CreateMap<UserCreationModel, EntityUser>()
                .ForMember(x=>x.UserName,opt=>opt.MapFrom(x=>x.Email));
        }
    }
}
