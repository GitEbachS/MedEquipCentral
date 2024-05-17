using System.ComponentModel.DataAnnotations;
namespace MedEquipCentral.DTO
{
    public class OrderUpdateDto
    {
        public int CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public int CVV { get; set; }
        public int Zip { get; set; }
    }
}
