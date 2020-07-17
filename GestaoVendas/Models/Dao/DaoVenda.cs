using System.Collections.Generic;
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
    }
}
