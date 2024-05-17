using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.Models;

public class Review
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime DateCreated { get; set; }
    public string CommentReview { get; set; }
}
