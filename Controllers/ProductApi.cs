using MedEquipCentral.Models;
using MedEquipCentral.DTO;
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

            //view product details
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

            //delete product
            app.MapDelete("/products/{productId}", (MedEquipCentralDbContext db, int productId) =>
            {
                var product = db.Products.Find(productId);
                if (product == null)
                {
                    return Results.NotFound();
                }

                db.Products.Remove(product);
                db.SaveChanges();
                return Results.NoContent();
            });

            //update Product
            app.MapPut("/products/update/{id}", (MedEquipCentralDbContext db, int id, ProductDto productToUpdateDto) =>
{
                Product productToUpdate = db.Products.SingleOrDefault(product => product.Id == id);
                if (productToUpdate == null)
                {
                    return Results.NotFound();
                }
                productToUpdate.Name = productToUpdateDto.Name;
                productToUpdate.CatId = productToUpdateDto.CatId;
                productToUpdate.Image = productToUpdateDto.Image;
                productToUpdate.Description = productToUpdateDto.Description;
                productToUpdate.Price = productToUpdateDto.Price;

                db.SaveChanges();
                return Results.NoContent();
            });

            //create Product
            app.MapPost("/product/new", (MedEquipCentralDbContext db, ProductDto productDto) =>
            {
                // Create a new Product entity from the provided DTO
                var newProduct = new Product
                {
                    Name = productDto.Name,
                    Image = productDto.Image,
                    CatId = productDto.CatId,
                    Description = productDto.Description,
                    Price = productDto.Price
                };

                // Add the new product to the database
                db.Products.Add(newProduct);
                db.SaveChanges();

                return Results.Created($"/product/new/{newProduct.Id}", newProduct);
            });

           
            //Search products by Name, Description, or Category Name
            app.MapGet("/products/search/{query}", (MedEquipCentralDbContext db, string query) =>
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return Results.BadRequest("Search query cannot be empty");
                }

                var filteredProducts = db.Products
                                                    .Include(p => p.Category)
                                    .Where(p => p.Name.ToLower().Contains(query.ToLower()) ||
                                    p.Description.ToLower().Contains(query.ToLower()) ||
                                    p.Category.Name.ToLower().Contains(query.ToLower()))
                                    .ToList();

                if (filteredProducts.Count == 0)
                {
                    return Results.NotFound("No products found for the given search query.");
                }
                else
                {
                    return Results.Ok(filteredProducts);
                }
            });

        }
    }
}
