using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.DTO
{
    public class UpdateOrderProductQuantityDTO
    {
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
