using MedEquipCentral.Models;
using Microsoft.AspNetCore.Mvc;
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
                            product.Image,
                            product.Description,
                            product.Price,
                            Category = new { product.Category.Id, product.Category.Name }
                        })
                    })
                    .ToList();

                return Results.Ok(categoriesWithProducts);
            });

            // Controller action to filter products by category name
            app.MapGet("/categories/{categoryName}", (MedEquipCentralDbContext db, string categoryName) =>
            {
                var categoriesWithProducts = db.Categories
                    .Include(category => category.Products)
                    .Where(category => category.Name == categoryName)
                    .Select(category => new
                    {
                        category.Id,
                        category.Name,
                        Products = category.Products.Select(product => new
                        {
                            product.Id,
                            product.Name,
                            product.Image,
                            product.Description,
                            product.Price,
                            Category = new { product.Category.Id, product.Category.Name }
                        })
                    })
                    .ToList();

                return Results.Ok(categoriesWithProducts);
            });


        }

    }
}
