using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoVendas.Data;
using GestaoVendas.Models;

namespace GestaoVendas.Controllers
{
    public class FuncionalidadesController : Controller
    {
        private readonly GestaoVendasContext _context;

        public FuncionalidadesController(GestaoVendasContext context)
        {
            _context = context;
        }

        // GET: Funcionalidades
        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionalidade.ToListAsync());
        }

        // GET: Funcionalidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = await _context.Funcionalidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionalidade == null)
            {
                return NotFound();
            }

            return View(funcionalidade);
        }

        // GET: Funcionalidades/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionalidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFuncionalidade")] Funcionalidade funcionalidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionalidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionalidade);
        }

        // GET: Funcionalidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = await _context.Funcionalidade.FindAsync(id);
            if (funcionalidade == null)
            {
                return NotFound();
            }
            return View(funcionalidade);
        }

        // POST: Funcionalidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFuncionalidade")] Funcionalidade funcionalidade)
        {
            if (id != funcionalidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionalidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionalidadeExists(funcionalidade.Id))
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
            return View(funcionalidade);
        }

        // GET: Funcionalidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionalidade = await _context.Funcionalidade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionalidade == null)
            {
                return NotFound();
            }

            return View(funcionalidade);
        }

        // POST: Funcionalidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionalidade = await _context.Funcionalidade.FindAsync(id);
            _context.Funcionalidade.Remove(funcionalidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionalidadeExists(int id)
        {
            return _context.Funcionalidade.Any(e => e.Id == id);
        }
    }
}
