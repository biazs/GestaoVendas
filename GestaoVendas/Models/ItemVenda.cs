using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("ItensVenda")]
    public class ItemVenda
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        public int VendaId { get; set; }

        public virtual Venda Venda { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }

        [Display(Name = "Quantidade de Produto")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int QuantidadeProduto { get; set; }

        [Display(Name = "Preço do Produco")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public double PrecoProduco { get; set; }
    }
}
