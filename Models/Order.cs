using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime? CloseDate { get; set; }
    public bool IsClosed { get; set; }
    public decimal Total => CalculateTotalPrice();
    public int TotalProducts => OrderProducts.Count;

    public List<OrderProduct> OrderProducts { get; set; }
    public int? CreditCardNumber { get; set; }
    public string? ExpirationDate { get; set; }
    public int? CVV { get; set; }
    public int? Zip { get; set; }

    // Method to calculate the total price
    private decimal CalculateTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var orderProduct in OrderProducts)
        {
            totalPrice += orderProduct.Product.Price;
        }
        return totalPrice;
    }
}
