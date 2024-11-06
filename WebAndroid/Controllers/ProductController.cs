using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAndroid.Data.Entities;
using WebAndroid.Data;
using WebAndroid.Interfaces;
using WebAndroid.Models.Product;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace WebAndroid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(
       AndroidDbContext context, IImageService imageHulk,
       IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public IActionResult GetList()
        {
            var list = context.Products
                .ProjectTo<ProductItemViewModel>(mapper.ConfigurationProvider)
                .ToList();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateViewModel model)
        {
            var entity = mapper.Map<ProductEntity>(model);
            context.Products.Add(entity);
            context.SaveChanges();

            if (model.Images != null)
            {
                var p = 1;
                foreach (var image in model.Images)
                {
                    var pi = new ProductImageEntity
                    {
                        Image = await imageHulk.SaveImageAsync(image),
                        Priority = p,
                        ProductId = entity.Id
                    };
                    p++;
                    context.ProductImages.Add(pi);
                }
                await context.SaveChangesAsync();
            }
            return Created();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = context.Products
                .Include(x => x.ProductImages)
                .SingleOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            if (product.ProductImages != null)
                foreach (var p in product.ProductImages)
                    imageHulk.DeleteImage(p.Image);

            context.Products.Remove(product);
            context.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await context.Products
                .ProjectTo<ProductItemViewModel>(mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromForm] ProductCreateViewModel model)
        {
            var request = this.Request;
            var product = await context.Products
                .Include(p => p.ProductImages).AsTracking()
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            mapper.Map(model, product);
            var imagesNames = model.Images.Where(x => x.ContentType == "old-image" ).Select(x => x.FileName) ?? [];
            if (imagesNames.Any()) 
            {
                var imagesToDelete = product?.ProductImages?.Where(x => !imagesNames.Contains(x.Image)) ?? [];
                if (imagesToDelete.Any())
                {
                    context.ProductImages.RemoveRange(imagesToDelete);
                    imageHulk.DeleteImages(imagesToDelete.Select(x => x.Image));
                }
            }
            
            if (model.Images is not null)
            {
                int index = 0;
                foreach (var image in model.Images)
                {
                    if (image.ContentType == "old-image")
                    {
                        var oldImage = product?.ProductImages?.FirstOrDefault(x => x.Image == image.FileName)!;
                        oldImage.Priority = index;
                    }
                    else
                    {
                        var imagePath = await imageHulk.SaveImageAsync(image);
                        context.ProductImages.Add(new ProductImageEntity
                        {
                            Image = imagePath,
                            ProductId = product.Id,
                            Priority = index
                        });
                    }
                    index++;
                }
            }
              
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
        
