using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Cnpj")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cnpj { get; set; }

        [Display(Name = "Contato")]
        public string Contato { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }
}
