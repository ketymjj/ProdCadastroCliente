using System.Threading.Tasks;
using ProjetoCliente.Domain;

namespace ProjetoCliente.Persistence.Contratos
{
    public interface IClientePersist
    {
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetClienteByIdAsync(int clienteId);
         
        Task<Cliente> GetClienteByCpfCnpjOrEmailAsync(string cpfCnpj, string email);

    }
}