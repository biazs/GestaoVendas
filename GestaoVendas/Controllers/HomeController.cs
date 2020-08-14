using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GestaoVendas.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GestaoVendasContext _context;

        public HomeController(ILogger<HomeController> logger, GestaoVendasContext context)
        {
            _logger = logger;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            // Validar quais funcionalidades o perfil logado poderá acessar             
            try
            {
                ViewBag.TemAcessoProdutos = true;
                ViewBag.TemAcessoClientes = true;
                ViewBag.TemAcessoFornecedores = true;
                ViewBag.TemAcessoVendedores = true;
                ViewBag.TemAcessoVendas = true;
                ViewBag.TemAcessoRelatorios = true;
                ViewBag.TemAcessoConfiguracao = true;

                //Verifica se usuário logado é administrador
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var perfilUsuario = _context.PerfilUsuario.FirstOrDefault(x => x.UserId == userId);
                if (perfilUsuario != null)
                {
                    var usuario = _context.TipoUsuario.FirstOrDefault(x => x.Id == perfilUsuario.IdTipoUsuario);

                    if (usuario != null && usuario.NomeTipoUsuario == "Administrador")
                    {
                        return View();
                    }
                }

                if (TemAcesso("Listar produto").Result.Equals(false))
                    ViewBag.TemAcessoProdutos = false;

                if (TemAcesso("Clientes").Result.Equals(false))
                    ViewBag.TemAcessoClientes = false;

                if (TemAcesso("Fornecedores").Result.Equals(false))
                    ViewBag.TemAcessoFornecedores = false;

                if (TemAcesso("Vendedores").Result.Equals(false))
                    ViewBag.TemAcessoVendedores = false;

                if (TemAcesso("Vendas").Result.Equals(false))
                    ViewBag.TemAcessoVendas = false;

                if (TemAcesso("Relatorios").Result.Equals(false))
                    ViewBag.TemAcessoRelatorios = false;

                if (TemAcesso("Configuracao").Result.Equals(false))
                    ViewBag.TemAcessoConfiguracao = false;

                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao carregar sistema. Tente novamente mais tarde. \n\n" + e.Message });
            }
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

        public IActionResult Video()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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
