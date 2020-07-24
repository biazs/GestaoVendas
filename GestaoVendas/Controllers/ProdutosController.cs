using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Models;
using GestaoVendas.Models.Dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace GestaoVendas.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly GestaoVendasContext _context;
        private readonly DaoProduto _daoProduto;

        public ProdutosController(GestaoVendasContext context, DaoProduto daoProduto)
        {
            _context = context;
            _daoProduto = daoProduto;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            try
            {
                List<Produto> listaProdutos = _daoProduto.ListarTodosProdutos();


                //string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", @Html.DisplayFor(modelItem => item.PrecoUnitario))

                List<Produto> lista = new List<Produto>();
                Produto item;
                foreach (var ls in listaProdutos)
                {
                    var x = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", ls.PrecoUnitario);
                    item = new Produto
                    {
                        Id = ls.Id,
                        Nome = ls.Nome,
                        Descricao = ls.Descricao,
                        //PrecoUnitario = Convert.ToDouble(x, CultureInfo.InvariantCulture),
                        //PrecoUnitario = double.Parse(x),
                        PrecoUnitario = ls.PrecoUnitario,
                        Quantidade = ls.Quantidade,
                        UnidadeMedida = ls.UnidadeMedida,
                        LinkFoto = ls.LinkFoto
                    };
                    lista.Add(item);
                }

                return View(lista);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao carregar registros. Tente novamente mais tarde. \n\n" + e.Message });
            }
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

            //Buscar quantidade na tabela estoque
            BuscarQuantidade(id);

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int quantidade, [Bind("Id,Nome,Descricao,PrecoUnitario,UnidadeMedida,LinkFoto,FornecedorId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);

                //inserir na tabela estoque
                var estoque = new Estoque() { Quantidade = quantidade };
                _context.Estoque.Add(estoque);

                await _context.SaveChangesAsync();

                //recuperar id do produto
                var id_produto = _context.Produto.OrderByDescending(o => o.Id).First().Id;

                //recuperar id do estoque                
                var id_estoque = _context.Estoque.OrderByDescending(o => o.Id).First().Id;

                //inserir na tabela produto_estoque
                var produto_estoque = new ProdutoEstoque() { EstoqueId = id_estoque, ProdutoId = id_produto };
                _context.ProdutoEstoque.Add(produto_estoque);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Nome", produto.FornecedorId);
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

            //Buscar quantidade na tabela estoque
            BuscarQuantidade(id);

            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        private void BuscarQuantidade(int? id)
        {
            try
            {
                //Buscar quantidade na tabela estoque
                var id_estoque = _context.ProdutoEstoque.Where(e => e.ProdutoId == id).Select(e => e.EstoqueId).FirstOrDefault();
                ViewBag.Quantidade = _context.Estoque.Where(e => e.Id == id_estoque).Select(e => e.Quantidade).FirstOrDefault();
            }
            catch
            {
                ViewBag.Quantidade = 0;
            }
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int quantidade, [Bind("Id,Nome,Descricao,PrecoUnitario,UnidadeMedida,LinkFoto,FornecedorId")] Produto produto)
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

                    //recuperar id do estoque
                    var id_estoque = _context.ProdutoEstoque.Where(e => e.ProdutoId == id).Select(e => e.EstoqueId).FirstOrDefault();

                    //inserir na tabela estoque
                    var estoque = _context.Estoque.First(a => a.Id == id_estoque);
                    estoque.Quantidade = quantidade;

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
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedor, "Id", "Nome", produto.FornecedorId);
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

            //Buscar quantidade na tabela estoque
            BuscarQuantidade(id);

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

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

    }
}
