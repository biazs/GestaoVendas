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

        /* public List<GraficoProdutos> RetornarGrafico()
         {
             DAL objDAL = new DAL();
             string sql = "SELECT SUM(qtde_produto) as qtde, p.nome as produto " +
                          "FROM sistema_venda.itens_venda i inner join sistema_venda.produto p " +
                          "on i.produto_id = p.id GROUP BY p.nome";

             DataTable dt = objDAL.RetDataTable(sql);

             List<GraficoProdutos> lista = new List<GraficoProdutos>();
             GraficoProdutos item;

             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 item = new GraficoProdutos();
                 item.QtdeVendido = double.Parse(dt.Rows[i]["qtde"].ToString());
                 item.DescricaoProduto = dt.Rows[i]["produto"].ToString();

                 lista.Add(item);
             }

             objDAL.FecharConexao();

             return lista;
         }*/
    }
}
