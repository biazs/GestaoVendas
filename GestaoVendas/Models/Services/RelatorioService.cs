using System;
using System.Collections.Generic;
using System.Linq;
using GestaoVendas.Data;
using GestaoVendas.Models.Dao;

namespace GestaoVendas.Models.Services
{
    public class RelatorioService
    {
        private readonly GestaoVendasContext _context;
        private readonly DaoProduto _daoProduto;

        public RelatorioService(GestaoVendasContext context, DaoProduto daoProduto)
        {
            _context = context;
            _daoProduto = daoProduto;
        }


        public List<GraficoProdutos> RetornarGrafico()
        {
            var listaProdutos = (from i in _context.ItensVenda
                                 join p in _context.Produto on i.ProdutoId equals p.Id
                                 select new
                                 {
                                     p.Nome,
                                     i.QuantidadeProduto
                                 })
                                 .GroupBy(t => t.Nome)
                                 .Select(gp => new
                                 {
                                     Nome = gp.Key,
                                     QuantidadeProduto = gp.Sum(c => c.QuantidadeProduto),
                                 });


            List<GraficoProdutos> lista = new List<GraficoProdutos>();
            GraficoProdutos item;

            foreach (var ls in listaProdutos)
            {
                item = new GraficoProdutos();
                item.QtdeVendido = ls.QuantidadeProduto;
                item.DescricaoProduto = ls.Nome;

                lista.Add(item);
            }

            return lista;
        }


        public List<VendasPorVendedor> RetornarVendasPorVendedor(int mes, int ano)
        {
            var listaProdutos = (from v1 in _context.Venda
                                 join v2 in _context.Vendedor on v1.VendedorId equals v2.Id
                                 where v1.Data.Month == mes && v1.Data.Year == ano
                                 select new
                                 {
                                     v2.Nome,
                                     v1.VendedorId
                                 })
                                .GroupBy(t => t.VendedorId)
                                .Select(gp => new
                                {
                                    Nome = gp.Key,
                                    QtdeVendido = gp.Sum(c => c.VendedorId),
                                });


            List<VendasPorVendedor> lista = new List<VendasPorVendedor>();
            VendasPorVendedor item;

            foreach (var ls in listaProdutos)
            {
                item = new VendasPorVendedor();
                item.Vendedor = ls.Nome.ToString();
                item.QtdeVendido = ls.QtdeVendido;


                lista.Add(item);
            }

            return lista;
        }

        public List<EstoqueProduto> RetornarListaEstoque()
        {
            var listaProdutosEstoque = (from p in _context.Produto
                                        join pe in _context.ProdutoEstoque on p.Id equals pe.ProdutoId
                                        orderby p.Nome
                                        select new
                                        {
                                            p.Id,
                                            p.Nome,
                                            pe.EstoqueId
                                        }).ToList();




            List<EstoqueProduto> lista = new List<EstoqueProduto>();
            EstoqueProduto item;

            foreach (var ls in listaProdutosEstoque)
            {
                item = new EstoqueProduto();
                item.Id = Convert.ToInt32(ls.Id);
                item.Nome = ls.Nome.ToString();

                var qtde = _context.Estoque.FirstOrDefault(m => m.Id == ls.EstoqueId).Quantidade;

                item.Quantidade = qtde;


                lista.Add(item);
            }

            return lista;
        }
    }
}
