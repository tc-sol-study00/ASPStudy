using Microsoft.EntityFrameworkCore;

namespace ASPEnshu.Data
{
    public class ASPEnshuContext : DbContext
    {
        public ASPEnshuContext (DbContextOptions<ASPEnshuContext> options)
            : base(options)
        {
        }
        public DbSet<ASPEnshu.Models.Education> Education { get; set; } = default!;
    }
}