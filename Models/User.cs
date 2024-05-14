using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public int JobFunctionId { get; set; }
    public JobFunction JobFunction { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsBizOwner { get; set; }
    public string Uid { get; set; }

    public List<Order> Orders { get; set; }
    public ICollection<Product> Products { get; set; }  //Favorite List
}
