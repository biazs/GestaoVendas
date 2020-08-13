using System;
using System.Collections.Generic;
using System.Linq;
using GestaoVendas.Data;

namespace GestaoVendas.Models.Dao
{
    public class DaoVenda
    {
        private readonly GestaoVendasContext _context;
        private readonly DaoProduto _daoProduto;

        public DaoVenda(GestaoVendasContext context, DaoProduto daoProduto)
        {
            _context = context;
            _daoProduto = daoProduto;
        }

        public List<Produto> RetornarListaProdutos()
        {
            return _daoProduto.ListarTodosProdutos();
        }

        //Para atender filtro do relatório
        public List<Venda> ListagemVendas(DateTime DataDe, DateTime DataAte)
        {
            return RetornarListagemVendas(DataDe, DataAte);
        }

        //Listagem Geral
        public List<Venda> ListagemVendas()
        {
            return RetornarListagemVendas(DateTime.Parse("1900-01-01"), DateTime.Parse("2300-01-01"));
        }

        private List<Venda> RetornarListagemVendas(DateTime DataDe, DateTime DataAte)
        {
            var listaVendas = from v1 in _context.Venda
                              join v2 in _context.Vendedor on v1.VendedorId equals v2.Id
                              join c in _context.Cliente on v1.ClienteId equals c.Id
                              where v1.Data >= DataDe && v1.Data <= DataAte
                              orderby v1.Data, v1.Id, v1.Total
                              select new
                              {
                                  v1.Id,
                                  v1.Data,
                                  v1.Total,
                                  v1.VendedorId,
                                  v1.ClienteId
                              };

            List<Venda> lista = new List<Venda>();
            Venda item;

            foreach (var ls in listaVendas)
            {
                item = new Venda
                {
                    Id = ls.Id,
                    Data = ls.Data,
                    Total = ls.Total,
                    ClienteId = ls.ClienteId,
                    VendedorId = ls.VendedorId
                };
                lista.Add(item);

            }

            return lista;
        }

    }
}
