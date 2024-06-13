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
                    OrderId = orderProductDto.OrderId,
                    Quantity = 1
                });

                db.SaveChanges();

                return Results.Ok("Product added to order successfully");
            });

            //get the orderProduct quantity in an order
            app.MapPut("/orderProduct/quantity", (MedEquipCentralDbContext db, UpdateOrderProductQuantityDTO dto) =>
            {
                var orderProduct = db.OrderProducts
                    .FirstOrDefault(op => op.ProductId == dto.ProductId && op.OrderId == dto.OrderId);

                if (orderProduct != null)
                {
                    orderProduct.Quantity = dto.Quantity;
                    orderProduct.OrderId = dto.OrderId;
                    orderProduct.ProductId = dto.ProductId;
                    db.SaveChanges();
                    return Results.Ok();
                }
                else
                {
                    return Results.NotFound();
                }
            });



            //delete product from an order
            app.MapDelete("/orders/removeProduct/{orderId}/{productId}", (MedEquipCentralDbContext db, int orderId, int productId) =>
            {
                OrderProduct orderProductToDelete = db.OrderProducts
                                                    .Where(op => op.ProductId == productId && op.OrderId == orderId)
                                                    .FirstOrDefault();

                if (orderProductToDelete == null)
                {
                    return Results.NotFound("OrderProduct not found");
                }

                db.OrderProducts.Remove(orderProductToDelete);
                db.SaveChanges();

                // Return 204 No Content upon successful deletion
                return Results.NoContent();
            });
        }
    }
}
