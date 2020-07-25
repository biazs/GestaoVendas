using System;
using System.Diagnostics;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    //[Authorize]
    public class ConfiguracaoController : BaseController
    {
        private readonly GestaoVendasContext _context;

        public ConfiguracaoController(GestaoVendasContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var temAcesso = await UsuarioTemAcesso("Configuracao", _context);

                //if (!temAcesso)
                //{
                //    return RedirectToAction("Index", "Home");
                //}

                //temAcesso = await UsuarioTemAcesso("Configuracao", _context);

                //if (!temAcesso)
                //{
                //    ViewBag.TemAcesso = false;
                //}

                ViewBag.TemAcesso = true;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao carregar registros. Tente novamente mais tarde. \n\n" + e.Message });
            }
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