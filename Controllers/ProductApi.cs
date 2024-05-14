using MedEquipCentral.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class ProductApi
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/products", (MedEquipCentralDbContext db) =>
            {
                var products = db.Products
                                .Include(p => p.Category)
                                .Select(p => new
                                {
                                    p.Id,
                                    p.Name,
                                    p.Description,
                                    p.Price,
                                    Category = new { p.Category.Id, p.Category.Name }
                                })
                                .ToList();

                return Results.Ok(products);
            });

            app.MapGet("/products/{productId})", (MedEquipCentralDbContext db, int productId) =>
            {
                var productDetails = db.Products
                    .Include(p => p.Category)
                    .Include(p => p.Reviews)
                        .ThenInclude(r => r.User) 
                    .Include(p => p.SimilarItems) 
                        .ThenInclude(sp => sp.SimilarProduct) 
                            .ThenInclude(sp => sp.Category)
                    .Where(p => p.Id == productId)
                    .Select(p => new
                    {
                        p.Id,
                        p.Name,
                        p.Description,
                        p.Price,
                        Category = new { p.Category.Id, p.Category.Name },
                        Reviews = p.Reviews.Select(r => new
                        {
                            r.Id,
                            r.Rating,
                            DateCreated = r.DateCreated,
                            r.CommentReview,
                            User = new { r.User.FirstName, r.User.LastName, r.User.Image } 
                        }),
                        SimilarProducts = p.SimilarItems.Select(si => new
                        {
                            SimilarProductId = si.SimilarProduct.Id,
                            SimilarProductName = si.SimilarProduct.Name,
                            SimilarProductDescription = si.SimilarProduct.Description,
                            SimilarProductPrice = si.SimilarProduct.Price,
                            SimilarProductCategory = new { si.SimilarProduct.Category.Id, si.SimilarProduct.Category.Name }
                        })
                    })
                    .SingleOrDefault();

                if (productDetails == null)
                {
                    return Results.NotFound(); 
                }

                return Results.Ok(productDetails);
            });





        }
    }
}
