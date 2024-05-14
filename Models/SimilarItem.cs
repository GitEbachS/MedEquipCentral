using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.Models;

public class SimilarItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int SimilarProductId { get; set; }
    public Product SimilarProduct { get; set; }
}
