using ProjetoCliente.Application.Dtos;

namespace ProjetoCliente.Application.Contratos
{
    public interface IClienteService
    {
        Task<ClienteDto> AddCliente(ClienteDto model);

        Task<ClienteDto> UpdateCliente(int clienteId, ClienteDto model);

        Task<bool> DeleteCliente(int clienteId);

         Task<List<ClienteDto>> GetAllClientesAsync();
         Task<ClienteDto> GetClienteByIdAsync(int clienteId);

    }
}