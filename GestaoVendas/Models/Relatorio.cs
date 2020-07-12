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

        /*public async Task<List<GraficoProdutos>> RetornarGrafico(GestaoVendasContext _context)
         {
             
             string sql = "SELECT SUM(qtde_produto) as qtde, p.nome as produto " +
                          "FROM itens_venda i " +
                          "inner join produto p " +
                          "on i.produto_id = p.id GROUP BY p.nome";





            string consulta = await(from i in _context.ItensVenda 
                                    join p in _context.Produto 
                                    on i.ProdutoId equals p.Id                                    
                                    select new
                                    {
                                        SUM(i.QuantidadeProduto) as qtde, p.Nome as produto
                                    }).AnyAsync();

            return consulta;



            List<GraficoProdutos> lista = new List<GraficoProdutos>();
             GraficoProdutos item;

             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 item = new GraficoProdutos();
                 item.QtdeVendido = double.Parse(dt.Rows[i]["qtde"].ToString());
                 item.DescricaoProduto = dt.Rows[i]["produto"].ToString();

                 lista.Add(item);
             }

             

             return lista;
         }*/
    }
}
