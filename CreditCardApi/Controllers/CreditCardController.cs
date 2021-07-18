
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CreditCardApi.Model;
using CreditCardApi.Interface;
using Microsoft.AspNetCore.Authorization;

namespace CreditCardApi.Controllers
{
   

    [Route("api/CreditCard")]
    [ApiController]
    [Authorize]
    public class CreditCardController : ControllerBase
    {

 
         private readonly ICreditCardService _cardService ;

        public CreditCardController( ICreditCardService cardService)
        {
            _cardService=cardService;
        }

        // GET: api/CreditCard

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCardItemDTO>>> GetCreditCardItems()
        {
            return await _cardService.GetCreditCardItems();
        }

        // GET: api/CreditCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCardItemDTO>> GetCreditCardItem(long id)
        {           
           return await _cardService.GetCreditCardItem(id);            
          
        }

        // PUT: api/CreditCard/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCardItem(long id, CreditCardItemDTO creditCardItemDTO)
        {
            return await  _cardService.PutCreditCardItem(id, creditCardItemDTO);
        }

        // POST: api/CreditCard
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreditCardItem>> PostCreditCardItem(CreditCardItemDTO creditCardItemDTO)
        {
             return await  _cardService.PostCreditCardItem(creditCardItemDTO);
        }

        // DELETE: api/CreditCard/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCardItem(long id)
        {
           return await  _cardService.DeleteCreditCardItem(id);
        }

       

    }   
}
