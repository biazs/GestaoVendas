using System.Linq;
using GestaoVendas.Data;

namespace GestaoVendas.Models.Dao
{
    public class DaoProdutoEstoque
    {
        private readonly GestaoVendasContext _context;

        public DaoProdutoEstoque(GestaoVendasContext context)
        {
            _context = context;
        }

        public int RetornarIdEstoque(int? id)
        {
            return _context.ProdutoEstoque.Where(e => e.ProdutoId == id).Select(e => e.EstoqueId).FirstOrDefault();
        }
    }
}
