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
        public string Ano { get; set; }
        public string Mes { get; set; }
        public string Vendedor { get; set; }
        public double QtdeVendido { get; set; }
    }

    public class VendasPorPeriodo
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Vendedor { get; set; }
        public string Cliente { get; set; }
        public double Total { get; set; }
    }

    public class EstoqueProduto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
    }

}
