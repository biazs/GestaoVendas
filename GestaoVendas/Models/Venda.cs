using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoVendas.Models
{
    [Table("Vendas")]
    public class Venda
    {
        public Venda()
        {
            ItensVenda = new HashSet<ItemVenda>();
        }

        [Key]
        [Display(Name = "Código")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int Id { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime Data { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public double Total { get; set; }

        [Display(Name = "Vendedor")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int VendedorId { get; set; }

        [ForeignKey("VendedorId")]
        public Vendedor Vendedor { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [NotMapped]
        public string ListaProdutos { get; set; }

        public virtual ICollection<ItemVenda> ItensVenda { get; set; }
    }

    public class VendaPdf
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Total { get; set; }
        public string NomeVendedor { get; set; }
        public string NomeCliente { get; set; }
    }
}
