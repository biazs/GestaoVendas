using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GestaoVendas.Libraries.Mensagem;

namespace GestaoVendas.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Cpf { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E004")]
        public string Email { get; set; }

        public virtual ICollection<Venda> Vendas { get; set; }
    }
}
