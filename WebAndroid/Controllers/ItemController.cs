using Microsoft.AspNetCore.Mvc;
using WebAndroid.Models;

namespace WebAndroid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private static readonly Item[] items =
        {
           new(){ Image = "1.jpg",Title = "Item 1" },
           new(){ Image = "2.jpg",Title = "Item 2" },
           new(){ Image = "3.jpg",Title = "Item 3" },
           new(){ Image = "4.jpg",Title = "Item 4" },
           new(){ Image = "5.jpg",Title = "Item 5" },
           new(){ Image = "6.jpg",Title = "Item 6" },
           new(){ Image = "7.jpg",Title = "Item 7" },
           new(){ Image = "8.jpg",Title = "Item 8" },
           new(){ Image = "9.jpg",Title = "Item 9" },
           new(){ Image = "10.jpg",Title = "Item 10" },
           new(){ Image = "11.jpg",Title = "Item 11" },

        };

       

        public ItemController(){}

        [HttpGet("/get")]
        public IEnumerable<Item> Get() => items;
        
    }
}
