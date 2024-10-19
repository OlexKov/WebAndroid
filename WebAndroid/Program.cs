using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebAndroid.Data;
using WebAndroid.Data.Entities;
using WebAndroid.Interfaces;
using WebAndroid.Repositories;
using WebAndroid.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AndroidDbContext>(opt=>opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

string imagesDirPath = Path.Combine(Directory.GetCurrentDirectory(), builder.Configuration["DirImages"]!);

if (!Directory.Exists(imagesDirPath))
{
    Directory.CreateDirectory(imagesDirPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesDirPath),
    RequestPath = "/images"
});

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AndroidDbContext>();
    var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
    //dbContext.Database.EnsureDeleted();
   // dbContext.Database.Migrate();

    if (!dbContext.Categories.Any())
    {
        int number = 10;
        var list = new Faker("uk").Commerce.Categories(number);
        List<Category> categories = [];
        foreach (var name in list)
        {
            string image = await imageService.SaveImageFromUrlAsync("https://picsum.photos/1200/800?category");
            categories.Add( new Category
            {
                Name = name,
                Description = new Faker("uk").Commerce.ProductDescription(),
                Image = image
            });
        }
        dbContext.Categories.AddRange(categories);
        dbContext.SaveChanges();
    }
}

app.Run();
