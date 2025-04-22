using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjetoCliente.Application.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Nome_RazaoSocial { get; set; } = string.Empty;

        public string TipoPessoa { get; set; } = string.Empty;
        public string CPF_CNPJ { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Phone(ErrorMessage = "O campo {0} está com número invalido.")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Display(Name = "e-mail")]
        [EmailAddress(ErrorMessage = "[É necessátrio se um e-mail válido.")]
        public string Email { get; set; } = string.Empty;

        public string CEP { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public int Numero { get; set; } 
        public string Bairro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
       
        [JsonPropertyName("ie")]
        public string? IE { get; set; }
        public bool IsentoIE { get; set; } // se IE for isento

        public int Status { get; set; }

    }
}