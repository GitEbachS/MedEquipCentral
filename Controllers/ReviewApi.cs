using MedEquipCentral.Models;
using MedEquipCentral.DTO;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class ReviewApi
    {
        public static void Map(WebApplication app)
        {

            //create new review
            app.MapPost("/reviews/create", (MedEquipCentralDbContext db, ReviewDto newReviewDto) =>
            {
                var product = db.Products.FirstOrDefault(p => p.Id == newReviewDto.ProductId);
                if (product == null)
                {
                    return Results.NotFound("Product not found");
                }

                var newReview = new Review
                {
                    Rating = newReviewDto.Rating,
                    CommentReview = newReviewDto.CommentReview,
                    UserId = newReviewDto.UserId,
                    ProductId = newReviewDto.ProductId,
                    DateCreated = DateTime.Now,
                };

                db.Reviews.Add(newReview);
                db.SaveChanges();

                return Results.NoContent();
            });

            //update Review
            app.MapPut("/reviews/update/{reviewId}", (MedEquipCentralDbContext db, int reviewId, ReviewDto updatedReviewDto) =>
            {
                var reviewToUpdate = db.Reviews.FirstOrDefault(r => r.Id == reviewId);
                if (reviewToUpdate == null)
                {
                    return Results.NotFound("Review not found");
                }

                reviewToUpdate.Rating = updatedReviewDto.Rating;
                reviewToUpdate.CommentReview = updatedReviewDto.CommentReview;

                db.SaveChanges();

                return Results.NoContent();
            });

            //get Reviews by userId
            app.MapGet("/review/{userId}", (MedEquipCentralDbContext db, int userId) =>
            {
                var userReviews = db.Reviews
                .Include(r => r.Product)
                .Where(r => r.UserId == userId)
                .Select(r => new
                {
                    r.Id,
                    r.Rating,
                    r.CommentReview,
                    DateCreated = r.DateCreated.ToString("MM/dd/yyyy"),
                    Product = new
                    {
                        r.Product.Id,
                        r.Product.Name,
                        r.Product.Image,
                        r.Product.Price
                    }
                })
                .ToList();

                if (userReviews.Count == 0)
                {
                    return Results.NotFound();
                }
                return Results.Ok(userReviews);
            });

            //get single review by reviewId
            app.MapGet("/singleReview/{reviewId}", (MedEquipCentralDbContext db, int reviewId) =>
            {
                var singleReview = db.Reviews
                    .Where(r => r.Id == reviewId)
                    .Select(r => new
                    {
                        r.Id,
                        r.Rating,
                        r.CommentReview,
                        DateCreated = r.DateCreated.ToString("MM/dd/yyyy"),
                        Product = new
                        {
                            r.Product.Id,
                            r.Product.Name,
                            r.Product.Image,
                            r.Product.Price
                        }
                    })
                    .SingleOrDefault();

                if (singleReview == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(singleReview);
            });


            //delete Review
            app.MapDelete("/reviews/delete/{reviewId}", (MedEquipCentralDbContext db, int reviewId) =>
            {
                var reviewToDelete = db.Reviews.FirstOrDefault(r => r.Id == reviewId);
                if (reviewToDelete == null)
                {
                    return Results.NotFound("Review not found");
                }

                db.Reviews.Remove(reviewToDelete);
                db.SaveChanges();

                return Results.NoContent();
            });


        }
    }
}
