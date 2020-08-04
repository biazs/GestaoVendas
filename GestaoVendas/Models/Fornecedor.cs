using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestaoVendas.Libraries.Mensagem;

namespace GestaoVendas.Models
{
    [Table("Fornecedores")]
    public class Fornecedor
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Nome { get; set; }

        [Display(Name = "Cnpj")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Cnpj { get; set; }

        [Display(Name = "Contato")]
        public string Contato { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }
}
