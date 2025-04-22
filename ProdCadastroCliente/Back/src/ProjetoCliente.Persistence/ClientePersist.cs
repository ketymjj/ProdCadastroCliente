using Microsoft.EntityFrameworkCore;
using ProjetoCliente.Domain;
using ProjetoCliente.Persistence.Contextos;
using ProjetoCliente.Persistence.Contratos;

namespace ProjetoCliente.Persistence
{
    public class ClientePersist : IClientePersist
    {

        public ProjetoClientesContext _context;

        public ClientePersist(ProjetoClientesContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int clienteId)
        {
            IQueryable<Cliente> query = _context.Clientes;

            query = query.AsNoTracking().OrderBy(e => e.Id)
                          .Where(e => e.Id == clienteId);

            return await query.FirstOrDefaultAsync();
        }
        
        public async Task<Cliente> GetClienteByCpfCnpjOrEmailAsync(string cpfCnpj, string email)
         {
          return await _context.Clientes
         .FirstOrDefaultAsync(c => c.CPF_CNPJ == cpfCnpj || c.Email.ToLower() == email.ToLower());
         }

    }
}