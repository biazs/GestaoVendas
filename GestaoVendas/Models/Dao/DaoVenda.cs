using System.Collections.Generic;
using GestaoVendas.Data;

namespace GestaoVendas.Models.Dao
{
    public class DaoVenda
    {
        public List<Produto> RetornarListaProdutos(GestaoVendasContext _context)
        {
            return new DaoProduto().ListarTodosProdutos(_context);
        }
    }
}
