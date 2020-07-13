using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("Vendedores")]
    public class Vendedor
    {
        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Usuário")]
        [ForeignKey("ItentityUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ICollection<Venda> Vendas { get; set; }
    }
}
