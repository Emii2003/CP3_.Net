using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CP3.Data.AppData
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<BarcoEntity> Barco { get; set; }
    }

}
