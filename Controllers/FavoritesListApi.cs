using MedEquipCentral.Models;
using MedEquipCentral.DTO;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class FavoritesListApi
    {
        public static void Map(WebApplication app)
        {
            //view user's favorite products, organized by their category names
            app.MapGet("/user/{userId}/favoritesList", (MedEquipCentralDbContext db, int userId) =>
            {
                var user = db.Users
                .Include(u => u.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    return Results.NotFound("User not found");
                }

                //Group products by Category

                var productListByCategory = user.Products
                .GroupBy(p => p.Category)
                .Select(group => new
                {
                    Category = group.Key.Name,
                    Products = group.Select(product => new
                    {
                        product.Id,
                        product.Name,
                        product.Description,
                        product.Price
                    })
                })
                .ToList();

                return Results.Ok(productListByCategory);
            });

            //add to Favorites List
            app.MapPost("/users/{userId}/favoritesList/{productId}", (MedEquipCentralDbContext db, int userId, int productId) =>
            {
                // Find the user
                var user = db.Users
                .Include(u => u.Products) //include user's favorites list
                .FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    return Results.NotFound("User not found");
                }

                // Find the product
                var product = db.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return Results.NotFound("Product not found");
                }

                //Check if the product already exists in the user's favorites list
                if (user.Products.Any(p => p.Id == productId))
                {
                    return Results.BadRequest("Product already exists in the favorites list");
                }

                user.Products.Add(product); //add product to favorites list
                db.SaveChanges();

                return Results.Created($"/users/{userId}/favoritesList/{productId}", null);
            });

            //delete from favorites list
            app.MapDelete("/users/{userId}/favoritesList/{productId}", (MedEquipCentralDbContext db, int userId, int productId) =>
            {
                var user = db.Users
                    .Include(u => u.Products)
                    .FirstOrDefault(u => u.Id == userId);

                if (user == null)
                {
                    return Results.NotFound("User not found");
                }

                var productToRemove = user.Products.FirstOrDefault(p => p.Id == productId);
                if (productToRemove == null)
                {
                    return Results.NotFound("Product not found in user's favorites list");
                }

                user.Products.Remove(productToRemove);
                db.SaveChanges();

                return Results.NoContent();
            });

        }
    }
}
