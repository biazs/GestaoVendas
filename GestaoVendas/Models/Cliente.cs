using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        public virtual ICollection<Venda> Vendas { get; set; }
    }
}
