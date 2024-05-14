using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public int CatId { get; set; }
    public Category Category { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public List<Review> Reviews { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public ICollection<User> Users { get; set; } //ShoppingList
    public List<SimilarItem> SimilarItems { get; set; }
}
