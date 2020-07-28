using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Libraries.Mensagem;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

namespace GestaoVendas.Controllers
{
    [Authorize]
    public class ClientesController : BaseController
    {
        private readonly GestaoVendasContext _context;

        public ClientesController(GestaoVendasContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            try
            {
                var temAcesso = await UsuarioTemAcesso("Clientes", _context);

                if (!temAcesso)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.TemAcesso = true;

                return View(await _context.Cliente.ToListAsync());
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao carregar registros. Tente novamente mais tarde. \n\n" + e.Message });
            }

        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cpf,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cpf,Email")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }


        public IActionResult VisualizarComoPDF()
        {
            CarregaLista();

            var pdf = new ViewAsPdf
            {
                ViewName = "VisualizarComoPDF",
                IsGrayScale = true,
                Model = ViewBag.ListaClientes
            };

            return pdf;
        }

        private void CarregaLista()
        {
            ViewBag.ListaClientes = _context.Cliente.ToList();
            //ClienteModel().ListarTodosClientes();
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
