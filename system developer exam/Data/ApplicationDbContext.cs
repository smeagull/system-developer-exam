using Microsoft.EntityFrameworkCore;
using system_developer_exam.Models;

namespace system_developer_exam.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)  
        {
            
        }
       
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
