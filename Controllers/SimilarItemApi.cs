using MedEquipCentral.Models;
using MedEquipCentral.DTO;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class SimilarItemApi
    {
        public static void Map(WebApplication app)
        {

            //view similar products by single product Id
            app.MapGet("/products/{productId}/similar", (MedEquipCentralDbContext db, int productId) =>
            {
                var product = db.Products
                    .Include(p => p.Reviews)
                    .Include(p => p.SimilarItems)
                        .ThenInclude(si => si.SimilarProduct)
                            .ThenInclude(sp => sp.Category)
                    .FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    return Results.NotFound("Product not found");
                }

                var similarProducts = product.SimilarItems.Select(si => new
                {
                    si.SimilarProduct.Id,
                    si.SimilarProduct.Name,
                    si.SimilarProduct.Image,
                    si.SimilarProduct.Description,
                    si.SimilarProduct.Price,
                    Category = new { si.SimilarProduct.Category.Id, si.SimilarProduct.Category.Name }
                    // Add other properties as needed
                }).ToList();

                return Results.Ok(similarProducts);
            });

            //Add Similar Product to Single Product
            app.MapPost("/products/{productId}/similar/{userId}", (MedEquipCentralDbContext db, int productId, int userId, SimilarProductDto similarProductDto) =>
            {
                //Check if user is admin
                var user = db.Users.FirstOrDefault(u => u.Id == userId && u.IsAdmin);

                if (user == null)
                {
                    return Results.Conflict("Only admins can add similar products");
                }

                //Check for the product in the database
                var product = db.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return Results.NotFound("Product Not Found");
                }

                //Create a new SimilarItem entity from the provided DTO
                var similarItem = new SimilarItem
                {
                    ProductId = productId,
                    SimilarProductId = similarProductDto.SimilarProductId,
                    UserId = userId
                };

                db.SimilarItems.Add(similarItem);
                db.SaveChanges();

                return Results.Created($"/products/{productId}/similar", similarItem);
            });

            //delete Similar Product from Single Product
            app.MapDelete("/products/{productId}/removeSimilarProduct/{similarProductId}/{userId}", (MedEquipCentralDbContext db, int productId, int userId, int similarProductId) =>
            {
                //Check if the User is an Admin
                var user = db.Users.FirstOrDefault(u => u.Id == userId);
                if (user == null || !user.IsAdmin)
                {
                    return Results.Conflict("Only Admin can delete a Similar Product");
                }

                //Check if the similar product exists
                var similarProduct = db.SimilarItems.FirstOrDefault(si => si.SimilarProductId == similarProductId && si.ProductId == productId);

                if (similarProduct == null)
                {
                    return Results.NotFound("Similar product not found");
                }

                db.SimilarItems.Remove(similarProduct);
                db.SaveChanges();

                return Results.Ok("Similar product removed successfully");
            });
        }
    }
}
