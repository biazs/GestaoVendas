using System.Diagnostics;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GestaoVendas.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GestaoVendasContext _context;

        public HomeController(ILogger<HomeController> logger, GestaoVendasContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            /*
             * TODO: Verificar se há usuário logado no sistema
             * Se não estiver logado, exibe tela de login return RedirectToAction("Login", "Home");
             */

            /*
             * TODO: Validar quais funcionalidades o perfil logado poderá acessar
             */
            return View();

        }

        public async Task<bool> TemAcesso(string funcionalidade)
        {
            var temAcesso = await UsuarioTemAcesso(funcionalidade, _context);

            if (!temAcesso)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
