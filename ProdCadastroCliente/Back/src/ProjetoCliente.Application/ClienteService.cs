using AutoMapper;
using ProjetoCliente.Application.Contratos;
using ProjetoCliente.Application.Dtos;
using ProjetoCliente.Domain;
using ProjetoCliente.Persistence.Contratos;

namespace ProjetoCliente.Application
{
    public class ClienteService : IClienteService
    {
     
        private readonly IGeralPersist _geralPersist;
        private readonly IClientePersist _clientePersist;
        private readonly IMapper _mapper;

        public ClienteService(IGeralPersist geralPersist, 
                            IClientePersist clientePersist,
                            IMapper mapper)
        {
            _clientePersist = clientePersist;
            _geralPersist = geralPersist;
            _mapper = mapper;
        }
       public async Task<ClienteDto> AddCliente(ClienteDto model)
       {
           // Regra 1 - verificar se CPF/CNPJ ou E-mail já existem
           var existente = await _clientePersist.GetClienteByCpfCnpjOrEmailAsync(model.CPF_CNPJ, model.Email);
           if (existente != null)
               throw new Exception("Já existe um cliente com esse CPF/CNPJ ou E-mail.");
         
            if (model.TipoPessoa == "F" && !string.IsNullOrEmpty(model.IE))
             {
                 throw new Exception("Pessoa física não pode ter IE preenchido.");
             }
           // Regra 2 - se for pessoa física, idade mínima de 18 anos
            if (model.TipoPessoa == "F")
            {                
                if (!model.DataNascimento.HasValue)
                    throw new Exception("Data de nascimento é obrigatória para pessoa física.");

                var idade = DateTime.Today.Year - model.DataNascimento.Value.Year;
                if (model.DataNascimento.Value.Date > DateTime.Today.AddYears(-idade)) idade--;

                if (idade < 18)
                    throw new Exception("A idade mínima para cadastro é 18 anos.");
            }
       
           // Regra 3 - se for pessoa jurídica, precisa de IE ou marcar isento
           if (model.TipoPessoa == "J")
           {
               if (string.IsNullOrWhiteSpace(model.IE) && !model.IsentoIE)
                   throw new Exception("Informe a inscrição estadual (IE) ou marque como isento.");
           }
       
           var cliente = _mapper.Map<Cliente>(model);
       
           _geralPersist.Add<Cliente>(cliente);
           if (await _geralPersist.SaveChangesAsync())
           {
               var retorno = await _clientePersist.GetClienteByIdAsync(cliente.Id);
               return _mapper.Map<ClienteDto>(retorno);
           }
       
           return null;
       }

      public async Task<ClienteDto> UpdateCliente(int clienteId, ClienteDto model)
      {
          try
          {
              var cliente = await _clientePersist.GetClienteByIdAsync(clienteId);
              if (cliente == null) return null;
  
              model.Id = cliente.Id;
  
              _mapper.Map(model, cliente);
  
              _geralPersist.Update<Cliente>(cliente);
  
              if (await _geralPersist.SaveChangesAsync())
              {
                  var clienteRetorno = await _clientePersist.GetClienteByIdAsync( cliente.Id);
  
                  return _mapper.Map<ClienteDto>(clienteRetorno);
              }
              return null;
          }
          catch (Exception ex)
          {
              throw new Exception(ex.Message);
          }
      }  

        public async Task<bool> DeleteCliente(int clienteId)
        {
            try
            {
                var cliente = await _clientePersist.GetClienteByIdAsync(clienteId);
                if (cliente == null) throw new Exception("Cliente para delete não encontrado");

                 _geralPersist.Delete<Cliente>(cliente);
                return await _geralPersist.SaveChangesAsync();

            }
             catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ClienteDto>> GetAllClientesAsync()
        {
            try
            {
                var clientes = await _clientePersist.GetAllClientesAsync();
                if (clientes == null) return null;
    
                return _mapper.Map<List<ClienteDto>>(clientes);

    
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ClienteDto> GetClienteByIdAsync(int clienteId)
        {
            try
            {
                var cliente = await _clientePersist.GetClienteByIdAsync(clienteId);
                if (cliente == null) throw null;

                var resultado = _mapper.Map<ClienteDto>(cliente);

                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}