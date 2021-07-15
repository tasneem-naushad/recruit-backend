using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreditCardApi.Model;
using Microsoft.Extensions.Logging;


namespace CreditCardApi.Controllers
{
   

    [Route("api/CreditCard")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {

         private readonly ILogger<CreditCardController> _logger;
         private readonly CreditCardContext _context;

        public CreditCardController(CreditCardContext context,ILogger<CreditCardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/CreditCard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CreditCardItemDTO>>> GetCreditCardItems()
        {
            return await _context.CreditCardItems.Select(x=> CreditCardItemDTO(x)).ToListAsync();
        }

        // GET: api/CreditCard/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreditCardItemDTO>> GetCreditCardItem(long id)
        {
            var creditCardItem = await _context.CreditCardItems.FindAsync(id);

            if (creditCardItem == null)
            {
                return NotFound();
            }

            return CreditCardItemDTO( creditCardItem);
        }

        // PUT: api/CreditCard/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCardItem(long id, CreditCardItemDTO creditCardItemDTO)
        {
            if (id != creditCardItemDTO.Id)
            {
                return BadRequest();
            }

          //  _context.Entry(creditCardItemDTO).State = EntityState.Modified;

            var creditCardItem = await _context.CreditCardItems.FindAsync(id);
            if (creditCardItem == null)
            {
                return NotFound();
            }
            creditCardItem.Id = creditCardItemDTO.Id;
            creditCardItem.Name = creditCardItemDTO.Name;
            creditCardItem.ExpiryDate = creditCardItemDTO.ExpiryDate;
            creditCardItem.CSV = creditCardItemDTO.CSV;
            creditCardItem.CreditCardNumber = creditCardItemDTO.CreditCardNumber;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CreditCard
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CreditCardItem>> PostCreditCardItem(CreditCardItemDTO creditCardItemDTO)
        {
        var creditCardItem = new CreditCardItem
            {
                CreditCardNumber = creditCardItemDTO.CreditCardNumber,
                Name = creditCardItemDTO.Name,
                ExpiryDate = creditCardItemDTO.ExpiryDate,
                CSV = creditCardItemDTO.CSV 
            };

            _context.CreditCardItems.Add(creditCardItem);
            await _context.SaveChangesAsync();

          //  return CreatedAtAction("GetCreditCardItem", new { id = creditCardItem.Id }, creditCardItem);
            return CreatedAtAction(nameof(GetCreditCardItem), new { id = creditCardItem.Id }, CreditCardItemDTO(creditCardItem));
        }

        // DELETE: api/CreditCard/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCardItem(long id)
        {
            var creditCardItem = await _context.CreditCardItems.FindAsync(id);
            if (creditCardItem == null)
            {
                return NotFound();
            }

            _context.CreditCardItems.Remove(creditCardItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CreditCardItemExists(long id)
        {
            return _context.CreditCardItems.Any(e => e.Id == id);
        }

        private static CreditCardItemDTO CreditCardItemDTO(CreditCardItem creditCardItem) =>
                new CreditCardItemDTO
                {
                    Id = creditCardItem.Id,
                    Name = creditCardItem.Name,
                    ExpiryDate = creditCardItem.ExpiryDate,
                     CreditCardNumber = creditCardItem.CreditCardNumber,
                    CSV = creditCardItem.CSV
                };
    }
}
