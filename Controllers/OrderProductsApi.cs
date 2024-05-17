using MedEquipCentral.Models;
using MedEquipCentral.DTO;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class OrderProductsApi
    {
        public static void Map(WebApplication app)
        {

            //add product to an order
            app.MapPost("/orders/addProduct", (MedEquipCentralDbContext db, AddProductToOrderDto orderProductDto) =>
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == orderProductDto.OrderId);
                if (order == null)
                {
                    return Results.NotFound("Order not found");
                }

                var product = db.Products.FirstOrDefault(p => p.Id == orderProductDto.ProductId);
                if (product == null)
                {
                    return Results.NotFound("Product not found");
                }


                //Add the product to the order
                order.OrderProducts.Add(new OrderProduct
                {
                    ProductId = orderProductDto.ProductId,
                    OrderId = orderProductDto.OrderId
                });

                db.SaveChanges();

                return Results.Ok("Product added to order successfully");
            });

            //delete product from an order
            app.MapDelete("/orders/remove-product/{orderId}/{productId}", (MedEquipCentralDbContext db, int orderId, int productId) =>
            {
                // Find the order by orderId
                var order = db.Orders.FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                {
                    return Results.NotFound("Order not found");
                }

                // Find the product by productId
                var product = db.Products.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                {
                    return Results.NotFound("Product not found");
                }

                // Find the order product entry to remove
                var orderProduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == productId);
                if (orderProduct == null)
                {
                    return Results.NotFound("Product not found in order");
                }

                // Remove the product from the order
                order.OrderProducts.Remove(orderProduct);

                db.SaveChanges();

                return Results.Ok("Product removed from order successfully");
            });
        }
    }
}
