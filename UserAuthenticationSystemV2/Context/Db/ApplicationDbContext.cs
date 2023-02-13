using Microsoft.EntityFrameworkCore;
using UserAuthenticationSystemV2.Models;

namespace UserAuthenticationSystemV2.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }
    }
}