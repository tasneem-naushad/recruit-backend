using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreditCardApi.Model;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CreditCardApi.Interface;
using NETCore.Encrypt;
namespace CreditCardApi.Service
{
    [Authorize]
    public class CreditCardService : ControllerBase, ICreditCardService
    {
        private readonly ILogger<CreditCardService> _logger;
        private readonly CreditCardContext _context;
      


        public CreditCardService(CreditCardContext context, ILogger<CreditCardService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ActionResult<IEnumerable<CreditCardItemDTO>>> GetCreditCardItems()
        {
            return await _context.CreditCardItems.Select(x => CreditCardItemDTO(x)).ToListAsync();
        }
        public async Task<ActionResult<CreditCardItemDTO>> GetCreditCardItem(long id)
        {
            var creditCardItem = await _context.CreditCardItems.FindAsync(id);


            if (creditCardItem == null)
            {
                _logger.LogInformation("GetCreditCardItem:Credit card not found {id}", id);
                return NotFound();
            }

            return CreditCardItemDTO(creditCardItem);
        }


        public async Task<IActionResult> PutCreditCardItem(long id, CreditCardItemDTO creditCardItemDTO)
        {
            if (id != creditCardItemDTO.Id)
            {
                return BadRequest();
            }


            var creditCardItem = await _context.CreditCardItems.FindAsync(id);
            if (creditCardItem == null)
            {
                _logger.LogInformation("PutCreditCardItem:Credit card not found {id}", id);
                return NotFound();
            }
            creditCardItem.Id = creditCardItemDTO.Id;
            creditCardItem.Name = creditCardItemDTO.Name;
            creditCardItem.ExpiryDate = creditCardItemDTO.ExpiryDate;
            creditCardItem.CSV = creditCardItemDTO.CSV;
            creditCardItem.CreditCardNumber = EncryptProvider.Md5(creditCardItemDTO.CreditCardNumber);
            creditCardItem.EncryptedCreditCard = EncryptProvider.Md5(creditCardItemDTO.CreditCardNumber);
            creditCardItem.EncryptedCSV  = EncryptProvider.Md5(creditCardItemDTO.CSV.ToString());

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!CreditCardItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError("PutCreditCardItem:Exception for id " + id.ToString() + " " + exception.Message);
                    throw;
                }
            }

            return NoContent();
        }
        public async Task<ActionResult<CreditCardItem>> PostCreditCardItem(CreditCardItemDTO creditCardItemDTO)
        {
            var creditCardItem = new CreditCardItem
            {
                CreditCardNumber = creditCardItemDTO.CreditCardNumber,
                Name = creditCardItemDTO.Name,
                ExpiryDate = creditCardItemDTO.ExpiryDate,
                CSV = creditCardItemDTO.CSV,
                EncryptedCreditCard = EncryptProvider.Md5(creditCardItemDTO.CreditCardNumber),
                EncryptedCSV  = EncryptProvider.Md5(creditCardItemDTO.CSV.ToString())
            };

            _context.CreditCardItems.Add(creditCardItem);
            await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetCreditCardItem", new { id = creditCardItem.Id }, creditCardItem);
            return CreatedAtAction(nameof(GetCreditCardItem), new { id = creditCardItem.Id }, CreditCardItemDTO(creditCardItem));
        }

        public async Task<IActionResult> DeleteCreditCardItem(long id)
        {
            var creditCardItem = await _context.CreditCardItems.FindAsync(id);
            if (creditCardItem == null)
            {
                _logger.LogInformation("DeleteCreditCardItem:Credit card not found {id}", id);
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
                   CSV = creditCardItem.CSV,
                   EncryptedCreditCard = creditCardItem.EncryptedCreditCard,
                   EncryptedCSV= creditCardItem.EncryptedCSV
               };
    }

}