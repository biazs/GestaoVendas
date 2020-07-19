using System;

namespace GestaoVendas.Models
{
    public class Relatorio
    {
        public DateTime DataDe { get; set; }
        public DateTime DataAte { get; set; }
    }

    public class GraficoProdutos
    {
        public double QtdeVendido { get; set; }
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }

    }

    public class VendasPorVendedor
    {
        public string Vendedor { get; set; }
        public double QtdeVendido { get; set; }
    }
}
