using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;
using WebAndroid.Data.Entities;
using WebAndroid.Helpers;
using WebAndroid.Interfaces;
using WebAndroid.Models;

namespace WebAndroid.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly UserManager<EntityUser> userManager;
        private readonly IJwtService jwtService;
        private readonly IImageService imageService;

        public AccountService(IMapper mapper, UserManager<EntityUser> userManager, IJwtService jwtService,IImageService imageService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.jwtService = jwtService;
            this.imageService = imageService;
        }
        public async Task<LoginResponse> CreateAsync(UserCreationModel model)
        {
            var user = mapper.Map<EntityUser>(model);
            if(model.ImageFile is not null)
                user.Photo = await imageService.SaveImageAsync(model.ImageFile);
            var result = await userManager.CreateAsync(user,model.Password);
            if (result.Succeeded)
                await userManager.AddToRoleAsync(user, Roles.User.ToString());
            return new() { Token = await jwtService.CreateTokenAsync(user) };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginRequest.Password))
                throw new HttpException("Invalid login or password", HttpStatusCode.BadRequest);
            return new() { Token = await jwtService.CreateTokenAsync(user) };
        }
    }
}
