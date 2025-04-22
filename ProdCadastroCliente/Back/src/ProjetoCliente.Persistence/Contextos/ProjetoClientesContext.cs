using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ProjetoCliente.Domain;

namespace ProjetoCliente.Persistence.Contextos
{
    public class ProjetoClientesContext : DbContext
    {

        public ProjetoClientesContext(DbContextOptions<ProjetoClientesContext> options)
         : base(options){ }
   
        public DbSet<Cliente> Clientes { get; set; }

    }
}