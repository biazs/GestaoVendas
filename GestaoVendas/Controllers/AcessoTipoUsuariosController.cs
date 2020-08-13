using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Libraries.Mensagem;
using GestaoVendas.Models;
using GestaoVendas.Models.Dao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestaoVendas.Controllers
{
    public class AcessoTipoUsuariosController : BaseController
    {
        private readonly GestaoVendasContext _context;
        private readonly DaoAcessoTipoUsuarios _daoAcessoTipoUsuarios;

        public AcessoTipoUsuariosController(GestaoVendasContext context, DaoAcessoTipoUsuarios daoAcessoTipoUsuarios)
        {
            _context = context;
            _daoAcessoTipoUsuarios = daoAcessoTipoUsuarios;
        }

        // GET: AcessoTipoUsuarios
        public async Task<IActionResult> Index()
        {
            //var gestaoVendasContext = _context.AcessoTipoUsuario.Include(a => a.TipoUsuario);
            //return View(await gestaoVendasContext.ToListAsync());

            try
            {
                List<AcessoTipoUsuario> lista = _daoAcessoTipoUsuarios.ListarTodosAcessos();

                return View(lista);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao carregar registros. Tente novamente mais tarde. \n\n" + e.Message });
            }

        }

        // GET: AcessoTipoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario
                .Include(a => a.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }

            return View(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios/Create
        public IActionResult Create()
        {
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario");
            ViewData["IdFuncionalidade"] = new SelectList(_context.Funcionalidade, "Id", "NomeFuncionalidade");

            return View();
        }

        // POST: AcessoTipoUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acessoTipoUsuario);
                await _context.SaveChangesAsync();
                TempData["MSG_S"] = Mensagem.MSG_S001;
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            ViewData["IdFuncionalidade"] = new SelectList(_context.Funcionalidade, "Id", "NomeFuncionalidade", acessoTipoUsuario.IdFuncionalidade);
            return View(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            ViewData["IdFuncionalidade"] = new SelectList(_context.Funcionalidade, "Id", "NomeFuncionalidade", acessoTipoUsuario.IdFuncionalidade);
            return View(acessoTipoUsuario);
        }

        // POST: AcessoTipoUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdFuncionalidade,IdTipoUsuario")] AcessoTipoUsuario acessoTipoUsuario)
        {
            if (id != acessoTipoUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(acessoTipoUsuario);
                    TempData["MSG_S"] = Mensagem.MSG_S001;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcessoTipoUsuarioExists(acessoTipoUsuario.Id))
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
            ViewData["IdTipoUsuario"] = new SelectList(_context.TipoUsuario, "Id", "NomeTipoUsuario", acessoTipoUsuario.IdTipoUsuario);
            ViewData["IdFuncionalidade"] = new SelectList(_context.Funcionalidade, "Id", "NomeFuncionalidade", acessoTipoUsuario.IdFuncionalidade);
            return View(acessoTipoUsuario);
        }

        // GET: AcessoTipoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var acessoTipoUsuario = await _context.AcessoTipoUsuario
                .Include(a => a.TipoUsuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acessoTipoUsuario == null)
            {
                return NotFound();
            }

            return View(acessoTipoUsuario);
        }

        // POST: AcessoTipoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var acessoTipoUsuario = await _context.AcessoTipoUsuario.FindAsync(id);
            _context.AcessoTipoUsuario.Remove(acessoTipoUsuario);
            await _context.SaveChangesAsync();
            TempData["MSG_S"] = Mensagem.MSG_S002;
            return RedirectToAction(nameof(Index));
        }

        private bool AcessoTipoUsuarioExists(int id)
        {
            return _context.AcessoTipoUsuario.Any(e => e.Id == id);
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
