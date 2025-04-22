using AutoMapper;
using ProjetoCliente.Application.Dtos;
using ProjetoCliente.Domain;

namespace ProjetoCliente.Application.Helpers
{
    public class ProjetoClienteProfile : Profile
    {
        public ProjetoClienteProfile()
        {
              CreateMap<Cliente, ClienteDto>();
              CreateMap<ClienteDto, Cliente>();

        }
    }
}