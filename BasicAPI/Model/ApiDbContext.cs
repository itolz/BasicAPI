using Microsoft.EntityFrameworkCore;

namespace BasicAPI.Model
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
            
        }

        public DbSet<FornecedorModel> Fornecedores {get; set;}
    }
}