using MedEquipCentral.Models;
using MedEquipCentral.DTO;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class OrderApi
    {
        public static void Map(WebApplication app)
        {
            
            //get Order total for the Checkout Page
            app.MapGet("/order/total/{orderId}/{userId}", (MedEquipCentralDbContext db, int orderId, int userId) =>
            {
                var order = db.Orders
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .SingleOrDefault(o => o.Id == orderId && o.UserId == userId);

                if (order == null)
                {
                    return Results.NotFound();
                }

                var orderTotal = new
                {
                    Total = order.Total
                };

                return Results.Ok(orderTotal);
            });

            //get the cart that is open to add products to it
            app.MapGet("/orders/{userId}", (MedEquipCentralDbContext db, int userId) =>
            {
                var openOrder = db.Orders
                    .Where(o => !o.IsClosed && o.UserId == userId)
                    .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                    .Select(o => new
                    {
                        OrderId = o.Id,
                        Products = o.OrderProducts.Select(op => op.ProductId).ToList()
                    })
                    .SingleOrDefault();

                if (openOrder == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(openOrder);
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
                        order.User.Image,
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
                    CloseDate = order.CloseDate.HasValue ? order.CloseDate.Value.ToString("MM/dd/yyyy") : null,
                    Products = order.OrderProducts.Select(op => new
                    {
                        op.Product.Id,
                        op.Product.Name,
                        op.Product.Image,
                        op.Product.CategoryId,
                        Category = new
                        {
                            op.Product.Category.Id,
                            op.Product.Category.Name
                        },
                        op.Product.Description,
                        op.Product.Price,
                        Quantity = op.Quantity
                    }).ToList()
                };

                return Results.Ok(orderDetails);
            });

            //update order
            app.MapPut("/orders/{userId}/update/{orderId}", (MedEquipCentralDbContext db, int orderId, int userId, OrderUpdateDto orderUpdateDto) =>
            {
                var orderToUpdate = db.Orders
                .SingleOrDefault(order => order.Id == orderId && order.UserId == userId && !order.IsClosed);
                if (orderToUpdate == null)
                {
                    return Results.NotFound();
                }

                // Update order details
                orderToUpdate.CreditCardNumber = orderUpdateDto.CreditCardNumber;
                orderToUpdate.ExpirationDate = orderUpdateDto.ExpirationDate;
                orderToUpdate.CVV = orderUpdateDto.CVV;
                orderToUpdate.Zip = orderToUpdate.Zip;
                orderToUpdate.CloseDate = DateTime.Now;
                orderToUpdate.IsClosed = true;

                db.SaveChanges();
                return Results.NoContent();
            });

            // Create or retrieve open order for a user
            app.MapPost("/orders/create/{userId}", async (MedEquipCentralDbContext db, int userId) =>
            {
                var openOrder = await db.Orders
                    .Where(o => o.UserId == userId && !o.IsClosed)
                    .FirstOrDefaultAsync();

                if (openOrder != null)
                {
                    return Results.Ok(openOrder);
                }

                var newOrder = new Order
                {
                    UserId = userId,
                    IsClosed = false
                    
                };

                db.Orders.Add(newOrder);
                await db.SaveChangesAsync();

                return Results.Created($"/orders/{newOrder.Id}", newOrder);
            });

            //Get client order history
            app.MapGet("/orderHistory/{userId}", (MedEquipCentralDbContext db, int userId) =>
            {
                var orderhistory = db.Orders
                                     .Include(o => o.OrderProducts)
                                        .ThenInclude(op => op.Product)
                                    .Where(o => o.UserId == userId)
                                    .ToList();

                return Results.Ok(orderhistory);
            });

            //view order history details
            app.MapGet("/orders/{userId}/single/{orderId}", (MedEquipCentralDbContext db, int userId, int orderId) =>
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
                        order.User.Image,
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
                    order.CreditCardNumber,
                    order.ExpirationDate,
                    order.CVV,
                    order.Zip,
                    order.Total,
                    order.TotalProducts,
                    CloseDate = order.CloseDate.HasValue ? order.CloseDate.Value.ToString("MM/dd/yyyy") : null,
                    Products = order.OrderProducts.Select(op => new
                    {
                        op.Product.Id,
                        op.Product.Name,
                        op.Product.Image,
                        op.Product.CategoryId,
                        Category = new
                        {
                            op.Product.Category.Id,
                            op.Product.Category.Name
                        },
                        op.Product.Description,
                        op.Product.Price,
                        Quantity = op.Quantity
                    }).ToList()
                };

                return Results.Ok(orderDetails);
            });

        }
    }
}
