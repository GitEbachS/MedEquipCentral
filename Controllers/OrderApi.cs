using MedEquipCentral.Models;
using MedEquipCentral.DTO;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class OrderApi
    {
        public static void Map(WebApplication app)
        {
            //create order
            app.MapPost("/newOrder/{userId}", (MedEquipCentralDbContext db, int userId) =>
            {
                var user = db.Users.Find(userId);

                if (user == null)
                {
                    return Results.NotFound();
                }

                // Create a new Order entity for the user
                var newOrder = new Order
                {
                    UserId = userId,
                    IsClosed = false,
                };

                db.Orders.Add(newOrder);
                db.SaveChanges();

                return Results.Created($"/orders/{newOrder.Id}", newOrder);
            });

            //view order details
            app.MapGet("/orders/{userId}/{orderId}", (MedEquipCentralDbContext db, int userId, int orderId) =>
            {
                // Retrieve the order with the specified ID for the given user
                var order = db.Orders
                    .Include(o => o.User)
                        .ThenInclude(u => u.JobFunction)
                    .Include(o => o.OrderProducts)
                        .ThenInclude(p => p.Product)
                        .ThenInclude(p => p.Category)
                    .SingleOrDefault(o => o.Id == orderId && o.UserId == userId);

                if (order == null)
                {
                    return Results.NotFound();
                }

                // Construct the response object with order details
                var orderDetails = new
                {
                    order.Id,
                    order.UserId,
                    User = new
                    {
                        order.User.Id,
                        order.User.FirstName,
                        order.User.LastName,
                        order.User.Email,
                        order.User.Address,
                        order.User.JobFunctionId,
                        order.User.IsAdmin,
                        order.User.IsBizOwner,
                        order.User.Uid,
                        JobFunction = new
                        {
                            order.User.JobFunction.Id,
                            order.User.JobFunction.Name
                        }
                    },
                    order.IsClosed,
                    order.Total,
                    order.TotalProducts,
                    Products = order.OrderProducts.Select(op => new
                    {
                        op.Product.Id,
                        op.Product.Name,
                        op.Product.Image,
                        op.Product.CatId,
                        Category = new
                        {
                            op.Product.Category.Id,
                            op.Product.Category.Name
                        },
                        op.Product.Description,
                        op.Product.Price
                    }).ToList()
                };

                return Results.Ok(orderDetails);
            });

            //update order
            app.MapPut("/orders/update/{id}", (MedEquipCentralDbContext db, int id, OrderUpdateDto orderUpdateDto) =>
            {
                var orderToUpdate = db.Orders.SingleOrDefault(order => order.Id == id);
                if (orderToUpdate == null)
                {
                    return Results.NotFound();
                }

                // Update order details
                orderToUpdate.CreditCardNumber = orderUpdateDto.CreditCardNumber;
                orderToUpdate.ExpirationDate = orderUpdateDto.ExpirationDate;
                orderToUpdate.CVV = orderUpdateDto.CVV;
                orderToUpdate.Zip = orderUpdateDto.Zip;
                orderToUpdate.CloseDate = DateTime.Now;
                orderToUpdate.IsClosed = true;

                db.SaveChanges();
                return Results.NoContent();
            }); 
        }
    }
}
