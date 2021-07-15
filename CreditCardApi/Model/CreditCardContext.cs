using Microsoft.EntityFrameworkCore;
namespace CreditCardApi.Model
{
    public class CreditCardContext : DbContext
    {
        public CreditCardContext(DbContextOptions<CreditCardContext> options) : base(options)
        {

        }
        public DbSet<CreditCardItem> CreditCardItems{get;set;}
    }
}