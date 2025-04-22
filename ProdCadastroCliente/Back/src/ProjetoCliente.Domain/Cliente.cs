
using System.Text.Json.Serialization;
using ProjetoCliente.Domain.Enum;

namespace ProjetoCliente.Domain
{
    public class Cliente
    {

        public int Id { get; set; }
        public string Nome_RazaoSocial { get; set; } = string.Empty;
        public TipoPessoa TipoPessoa { get; set; }
        public string CPF_CNPJ { get; set; } = string.Empty;
        public DateTime? DataNascimento { get; set; }
        public string Telefone { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string CEP { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public int Numero { get; set; } 
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;

        public string estado { get; set; } = string.Empty;

          public string IE { get; set; } = string.Empty;// para pessoa jurídica
          public bool IsentoIE { get; set; } // se IE for isento
        
          public Status Status { get; set; } 

    }
}