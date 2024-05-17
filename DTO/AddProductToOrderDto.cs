using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.DTO
{
    public class AddProductToOrderDto
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
