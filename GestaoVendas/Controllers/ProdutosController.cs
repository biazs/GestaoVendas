using System.Linq;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace GestaoVendas.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly GestaoVendasContext _context;
        private readonly EstoquesController _estoque;

        public ProdutosController(GestaoVendasContext context, EstoquesController estoque)
        {
            _context = context;
            _estoque = estoque;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var gestaoVendasContext = _context.Produto.Include(p => p.Fornecedor);
            return View(await gestaoVendasContext.ToListAsync());
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Cnpj");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,PrecoUnitario,Quantidade,UnidadeMedida,LinkFoto,FornecedorId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();

                //recuperar id do produto

                var id_produto = _context.Produto.OrderByDescending(o => o.Id).First().Id;
                //inserir na tabela estoque
                var estoque = new Estoque() { Quantidade = produto.Quantidade };
                _context.Estoque.Add(estoque);

                //recuperar id do estoque                
                var id_estoque = _context.Estoque.OrderByDescending(o => o.Id).First().Id;

                //inserir na tabela produto_estoque
                var produto_estoque = new ProdutoEstoque() { EstoqueId = id_estoque, ProdutoId = id_produto };
                _context.ProdutoEstoque.Add(produto_estoque);

                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Cnpj", produto.FornecedorId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Cnpj", produto.FornecedorId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,PrecoUnitario,UnidadeMedida,LinkFoto,FornecedorId")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Cnpj", produto.FornecedorId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produto
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produto.Any(e => e.Id == id);
        }

        public IActionResult VisualizarComoPDF()
        {
            CarregaLista();

            var pdf = new ViewAsPdf
            {
                ViewName = "VisualizarComoPDF",
                IsGrayScale = true,
                Model = ViewBag.ListaProdutos
            };

            return pdf;
        }

        private void CarregaLista()
        {
            ViewBag.ListaProdutos = _context.Produto.ToList();
        }
    }
}
