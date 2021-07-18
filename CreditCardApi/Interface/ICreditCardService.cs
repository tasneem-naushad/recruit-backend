using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreditCardApi.Model;
namespace CreditCardApi.Interface
{
 
     public interface ICreditCardService 
    {
          Task<ActionResult<IEnumerable<CreditCardItemDTO>>> GetCreditCardItems();
          Task<ActionResult<CreditCardItemDTO>> GetCreditCardItem(long id);
          Task<IActionResult> PutCreditCardItem(long id, CreditCardItemDTO creditCardItemDTO);
          Task<ActionResult<CreditCardItem>> PostCreditCardItem(CreditCardItemDTO creditCardItemDTO);
          Task<IActionResult> DeleteCreditCardItem(long id);
    }    
}