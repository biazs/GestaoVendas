﻿using System.Linq;
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
    //[Authorize]
    public class VendasController : BaseController
    {
        private readonly GestaoVendasContext _context;

        public VendasController(GestaoVendasContext context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            /* var temAcesso = await UsuarioTemAcesso("Vendas", _context);

             if (!temAcesso)
             {
                 ViewBag.TemAcesso = false;
                 return RedirectToAction("Index", "Home");
             }
             ViewBag.TemAcesso = true;
            */
            var gestaoVendasContext = _context.Venda.Include(v => v.Cliente).Include(v => v.Vendedor).OrderByDescending(v => v.Data);
            return View(await gestaoVendasContext.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }




        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Nome");

            // var userId = Convert.ToInt32(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            // ViewBag.Vendedor = _context.Vendedor.FirstOrDefaultAsync(m => m.Id == userId);
            CarregarDados();
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Total,VendedorId,ClienteId")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Cpf", venda.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Email", venda.VendedorId);
            CarregarDados();
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Cpf", venda.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Email", venda.VendedorId);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Total,VendedorId,ClienteId")] Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Cpf", venda.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Email", venda.VendedorId);
            return View(venda);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Cliente)
                .Include(v => v.Vendedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venda = await _context.Venda.FindAsync(id);
            _context.Venda.Remove(venda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(int id)
        {
            return _context.Venda.Any(e => e.Id == id);
        }

        public IActionResult VisualizarComoPDF()
        {
            CarregaLista();

            var pdf = new ViewAsPdf
            {
                ViewName = "VisualizarComoPDF",
                IsGrayScale = true,
                Model = ViewBag.ListaVendas
            };

            return pdf;
        }

        private void CarregaLista()
        {
            ViewBag.ListaVendas = _context.Venda.ToList();
        }

        private void CarregarDados()
        {
            ViewBag.ListaProdutos = new DaoVenda().RetornarListaProdutos(_context);
        }
    }
}
