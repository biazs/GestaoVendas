using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class ConfiguracaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
