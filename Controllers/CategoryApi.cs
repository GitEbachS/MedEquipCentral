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
                            product.Price
                        })
                    })
                    .ToList();

                return Results.Ok(categoriesWithProducts);
            });

            // Controller action to filter products by category name
            app.MapGet("/categories/{categoryName}", (MedEquipCentralDbContext db, string categoryName) =>
            {
                var products = db.Products
                                .Include(p => p.Category)
                                .Where(p => p.Category.Name == categoryName)
                                .Select(p => new
                                {
                                    p.Id,
                                    p.Name,
                                    p.Image,
                                    p.Description,
                                    p.Price,
                                    Category = new { p.Category.Id, p.Category.Name }
                                })
                                .ToList();

                return Results.Ok(products);
            });

        }

    }
}
