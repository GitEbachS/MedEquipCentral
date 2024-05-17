using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int CatId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
