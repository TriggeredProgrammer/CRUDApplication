using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace CRUDApplication.Models
{
    public class CustomerDbContext:DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext>options):base(options)
        {
            
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
