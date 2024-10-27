using AutoMapper;
using WebAndroid.Data.Entities;
using WebAndroid.Models;

namespace WebAndroid.Mappers
{
    public class EntityUserProfile:Profile
    {
        public EntityUserProfile() 
        {
            CreateMap<UserCreationModel, EntityUser>();
        }
    }
}
