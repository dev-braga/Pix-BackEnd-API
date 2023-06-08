using Microsoft.EntityFrameworkCore;
using Pix.Models;

namespace Pix.Context
{
    public class PixContext: DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Chave> Chave { get; set; }
        public DbSet<Transacao> Transacao { get; set; }

        public PixContext(DbContextOptions<PixContext> options) : base(options) { 
                        
        }


    }
}
