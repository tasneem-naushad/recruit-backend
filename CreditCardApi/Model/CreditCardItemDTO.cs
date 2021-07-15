using System.ComponentModel.DataAnnotations;
namespace CreditCardApi.Model
{
    public class CreditCardItemDTO
    {
        
        public long Id {get;set;}
        [Required]
        [StringLength(50, ErrorMessage = "Name must be fewer than 50 characters.")]
        public string Name { get; set; }

        [CreditCard]
        public string CreditCardNumber { get; set; }
        [Required]
        public int CSV { get; set; }

        [Required]
        public System.DateTime ExpiryDate { get; set; }
    }
}