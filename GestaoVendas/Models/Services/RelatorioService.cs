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


        //Para atender filtro do relatório
        public List<VendasPorVendedor> RetornarVendasPorVendedor(DateTime DataDe, DateTime DataAte)
        {
            return VendasPorVendedor(DataDe, DataAte);
        }

        //Listagem Geral
        public List<VendasPorVendedor> RetornarVendasPorVendedor()
        {
            return VendasPorVendedor(DateTime.Parse("1900-01-01"), DateTime.Parse("2300-01-01"));
        }

        public List<VendasPorVendedor> VendasPorVendedor(DateTime DataDe, DateTime DataAte)
        {
            var listaProdutos = (from v1 in _context.Venda
                                 join v2 in _context.Vendedor on v1.VendedorId equals v2.Id
                                 where v1.Data >= DataDe && v1.Data <= DataAte
                                 select new
                                 {
                                     v2.Nome,
                                     v1.VendedorId,
                                     //i.QuantidadeProduto
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
    }
}
