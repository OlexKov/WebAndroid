using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAndroid.Helpers;
using WebAndroid.Interfaces;
using WebAndroid.Models;

namespace WebAndroid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService categoryService = categoryService;

        [HttpGet("get")]
        public async Task<IActionResult> Get() => Ok(await categoryService.GetAllAsync());

        [HttpGet("get/{id:int}")]
        public async Task<IActionResult> GetById([FromRoute]int id) => Ok(await categoryService.GetByIdAsync(id));

        [Authorize(Roles =Roles.Admin)]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CategoryCreationModel model) => Ok(await categoryService.CreateUpdateAsync(model));

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] CategoryCreationModel model) => Ok(await categoryService.CreateUpdateAsync(model));

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id) 
        {
            await categoryService.DeleteAsync(id);
            return Ok();
        }

    }
}
