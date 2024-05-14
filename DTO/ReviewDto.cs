using System.ComponentModel.DataAnnotations;

namespace MedEquipCentral.DTO
{
    public class ReviewDto
    {
        public int Rating { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string DateCreated { get; set; }
        public string CommentReview { get; set; }
    }
}
