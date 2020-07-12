using System.Collections.Generic;
using System.Linq;
using GestaoVendas.Data;

namespace GestaoVendas.Models.Dao
{

    public class DaoProduto
    {

        public DaoProduto()
        {
        }


        public List<Produto> ListarTodosProdutos(GestaoVendasContext _context)
        {
            var listaProdutos = (from p in _context.Produto
                                 join fo in _context.Fornecedor on p.FornecedorId equals fo.Id
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
                                     p.LinkFoto
                                 });
            List<Produto> lista = new List<Produto>();
            Produto item;

            foreach (var ls in listaProdutos)
            {
                item = new Produto
                {
                    Id = ls.Id,
                    Nome = ls.Nome,
                    Descricao = ls.Descricao,
                    PrecoUnitario = ls.PrecoUnitario,
                    Quantidade = ls.Quantidade,
                    UnidadeMedida = ls.UnidadeMedida,
                    LinkFoto = ls.LinkFoto
                };
                lista.Add(item);

            }

            return lista;

        }
    }
}
