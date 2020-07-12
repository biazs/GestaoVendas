using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public Produto()
        {
            ProdutosEstoque = new HashSet<ProdutoEstoque>();
            ItensVenda = new HashSet<ItemVenda>();
        }

        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Preço Unitário")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public double PrecoUnitario { get; set; }

        [Display(Name = "Unidade de medida")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string UnidadeMedida { get; set; }

        [Display(Name = "Link da foto")]
        public string LinkFoto { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int FornecedorId { get; set; }

        [NotMapped]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }

        public virtual ICollection<Fornecedor> Fornecedores { get; set; }

        public virtual ICollection<ProdutoEstoque> ProdutosEstoque { get; set; }

        public virtual ICollection<ItemVenda> ItensVenda { get; set; }



    }


}
