using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("ProdutosEstoque")]
    public class ProdutoEstoque
    {
        [Display(Name = "Código do produto")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }

        [Display(Name = "Código do estoque")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int EstoqueId { get; set; }

        public virtual Estoque Estoque { get; set; }
    }
}
