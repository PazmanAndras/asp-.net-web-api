using Microsoft.EntityFrameworkCore; //db context ebből öröklődik

namespace WebApplicationCRUD.Data
{
    public class ApiDbContext : DbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
