using Microsoft.EntityFrameworkCore;
 
namespace Exam.Models
{
    public class MainContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
	
	public DbSet<User> User { get; set; }
	public DbSet<Product> Product { get; set; }

    public DbSet<Bid> Bid { get; set; }

    }
}
