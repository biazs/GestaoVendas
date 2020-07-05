using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("Estoque")]
    public class Estoque
    {
        public Estoque()
        {
            ProdutosEstoque = new HashSet<ProdutoEstoque>();
        }

        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Quantidade")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Quantidade { get; set; }

        public virtual ICollection<ProdutoEstoque> ProdutosEstoque { get; set; }

    }
}
