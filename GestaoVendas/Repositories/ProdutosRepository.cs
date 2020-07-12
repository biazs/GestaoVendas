using System.Collections.Generic;
using GestaoVendas.Data;
using GestaoVendas.Models;

namespace GestaoVendas.Src
{
    public class ProdutosRepository
    {
        private readonly GestaoVendasContext _context;

        public ProdutosRepository()
        {
        }
        public ProdutosRepository(GestaoVendasContext context)
        {
            _context = context;
        }


        public List<Produto> ListarTodosProdutos()
        {
            List<Produto> lista = new List<Produto>();
            Produto item;
            // BuscarQuantidade e fornecedor
            /*var listaProdutos = (from p in _context.Produto
                                 join f in _context.Fornecedor on p.FornecedorId equals f.Id
                                 join pe in _context.ProdutoEstoque on p.Id equals pe.ProdutoId
                                 join e in _context.Estoque on pe.EstoqueId equals e.Id
                                 orderby p.Nome
                                 select new
                                 {
                                     p.Id,
                                     p.Nome,
                                     p.Descricao,
                                     p.PrecoUnitario,
                                     e.Quantidade,
                                     p.UnidadeMedida,
                                     p.LinkFoto,
                                     f.Cnpj,
                                 }).ToList();

            for (int i = 0; i < listaProdutos.Rows.Count; i++)
            {
                item = new Produto
                {
                    Id = listaProdutos.Rows[i]["id"].ToString(),
                    Nome = listaProdutos.Rows[i]["nome"].ToString(),
                    Descricao = listaProdutos.Rows[i]["descricao"].ToString(),
                    PrecoUnitario = float.Parse(listaProdutos.Rows[i]["preco_unitario"].ToString()),
                    Quantidade = int.Parse(listaProdutos.Rows[i]["quantidade"].ToString()),
                    UnidadeMedida = listaProdutos.Rows[i]["unidade_medida"].ToString(),
                    FornecedorId = listaProdutos.Rows[i]["nome_fornecedor"].ToString(),
                    LinkFoto = listaProdutos.Rows[i]["link_foto"].ToString(),
                };
                lista.Add(item);
            }*/

            return lista;


        }
    }
}
