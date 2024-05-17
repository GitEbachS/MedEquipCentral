using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.Models;

public class Product
{

    public List<Review> Reviews { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public ICollection<User> Users { get; set; } //ShoppingList
    public List<SimilarItem> SimilarItems { get; set; }
}
