using System.ComponentModel.DataAnnotations;
namespace CreditCardApi.Model
{

    public class CreditCardItem
    {
        public long Id {get;set;}
        [Required]
        [StringLength(50, ErrorMessage = "Name must be fewer than 50 characters.")]
        public string Name { get; set; }
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }
        [Required]
        public int CSV { get; set; }

        [Required]
        public System.DateTime ExpiryDate { get; set; }
        public string CreditCardStatus {get;set;}
        public string EncryptedCreditCard {get;set;}
        public string EncryptedCSV {get;set;}
    }
}
