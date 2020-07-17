using System.Collections.Generic;
using GestaoVendas.Data;
using GestaoVendas.Models;
using GestaoVendas.Models.Dao;
using Xunit;

namespace GestaoVendasTestes
{
    public class UnitTestModels
    {
        private readonly GestaoVendasContext _context;
        private readonly DaoProduto _daoProduto;

        public UnitTestModels(GestaoVendasContext context, DaoProduto daoProduto)
        {
            _context = context;
            _daoProduto = daoProduto;
        }


        [Fact]
        public void CheckTypeListaProdutos()
        {
            var lista = _daoProduto.ListarTodosProdutos();
            Assert.IsType<List<Produto>>(lista);

        }
    }
}
