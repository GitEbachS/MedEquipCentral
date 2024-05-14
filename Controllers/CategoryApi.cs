using MedEquipCentral.Models;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class CategoryApi
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/categories", (MedEquipCentralDbContext db) =>
            {
                var categoriesWithProducts = db.Categories
                    .Include(category => category.Products)
                    .Select(category => new
                    {
                        category.Id,
                        category.Name,
                        Products = category.Products.Select(product => new
                        {
                            product.Id,
                            product.Name,
                            product.Description,
                            product.Price
                        })
                    })
                    .ToList();

                return Results.Ok(categoriesWithProducts);
            });  
        }

    }
}
