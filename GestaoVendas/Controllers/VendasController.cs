using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using GestaoVendas.Data;
using GestaoVendas.Models;
using GestaoVendas.Models.Dao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rotativa.AspNetCore;

namespace GestaoVendas.Controllers
{
    [Authorize]
    public class VendasController : BaseController
    {
        private readonly GestaoVendasContext _context;
        private DaoProdutoEstoque _daoProdutoEstoque;
        private DaoVenda _daoVenda;

        public VendasController(GestaoVendasContext context, DaoProdutoEstoque daoProdutoEstoque, DaoVenda daoVenda)
        {
            _context = context;
            _daoProdutoEstoque = daoProdutoEstoque;
            _daoVenda = daoVenda;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            try
            {
                var temAcesso = await UsuarioTemAcesso("Vendas", _context);

                if (!temAcesso)
                {
                    return RedirectToAction("Index", "Home");
                }

                temAcesso = await UsuarioTemAcesso("Cancelar Venda", _context);

                if (!temAcesso)
                {
                    ViewBag.TemAcesso = false;
                }

                ViewBag.TemAcesso = true;
                var gestaoVendasContext = _context.Venda.Include(v => v.Cliente).Include(v => v.Vendedor).OrderByDescending(v => v.Data);
                return View(await gestaoVendasContext.ToListAsync());
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao carregar registros. Tente novamente mais tarde. \n\n" + e.Message });
            }
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var venda = await _context.Venda
                    .Include(v => v.Cliente)
                    .Include(v => v.Vendedor)
                    .FirstOrDefaultAsync(m => m.Id == id);

                CarregaListaItemVenda(id);

                if (venda == null)
                {
                    return NotFound();
                }

                return View(venda);
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro ao detalhar registros. Tente novamente mais tarde. \n\n" + e.Message });
            }
        }




        // GET: Vendas/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome");
                ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Nome");

                //Buscar id do usuário(Vendedor) logado
                if (User.Identity.IsAuthenticated)
                {
                    var userName = User.Identity.Name;
                    var usuario = _context.Users.FirstOrDefault(x => x.Email == userName);

                    if (usuario != null)
                    {
                        // TODO: fazer relacionamento da tabela users com Vendedores
                        ViewBag.Vendedor = _context.Vendedor.FirstOrDefault(m => m.Email == userName);
                    }
                }

                CarregarDados();
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro no registro de vendas. Tente novamente mais tarde. \n\n" + e.Message });
            }
        }


        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Total,VendedorId,ClienteId,ListaProdutos")] Venda venda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    venda.Data = DateTime.Now;

                    var tam = venda.Total.ToString().Count() - 2;
                    var total = venda.Total.ToString().Insert(tam, ".");

                    venda.Total = Convert.ToDouble(total);

                    //inserir na tabela venda
                    _context.Add(venda);

                    await _context.SaveChangesAsync();

                    //recuperar id da venda
                    var id_venda = _context.Venda.OrderByDescending(o => o.Id).First().Id;


                    //Serializar o JSON da lista de produtos selecionados e gravar na tabela itens_venda
                    List<ItemVenda> lista_produtos = JsonConvert.DeserializeObject<List<ItemVenda>>(venda.ListaProdutos);
                    for (var i = 0; i < lista_produtos.Count; i++)
                    {
                        //inserir na tabela ItensVenda
                        var itemVenda = new ItemVenda()
                        {
                            VendaId = id_venda,
                            ProdutoId = lista_produtos[i].ProdutoId,
                            QuantidadeProduto = lista_produtos[i].QuantidadeProduto,
                            PrecoProduco = lista_produtos[i].PrecoProduco
                        };

                        _context.ItensVenda.Add(itemVenda);

                        //recuperar id do estoque                        
                        var id_estoque = _daoProdutoEstoque.RetornarIdEstoque(itemVenda.ProdutoId);

                        //Baixar quantidade do estoque
                        var estoque = _context.Estoque.Find(id_estoque);

                        estoque.Quantidade = estoque.Quantidade - itemVenda.QuantidadeProduto;
                        _context.Estoque.Update(estoque);

                        await _context.SaveChangesAsync();
                    }
                }
                catch (Exception e)
                {
                    return RedirectToAction(nameof(Error), new { message = "Erro ao registar venda. Tente novamente mais tarde. \n\n" + e.Message });
                }
            }
            else if (venda.ListaProdutos == null)
            {
                TempData["MSG"] = "Lista de produtos vazia. Adicione algum item para registrar a venda.";
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", venda.ClienteId);
                ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Email", venda.VendedorId);
                CarregarDados();
                return View();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Nome", venda.ClienteId);
            ViewData["VendedorId"] = new SelectList(_context.Vendedor, "Id", "Email", venda.VendedorId);
            CarregarDados();
            return RedirectToAction(nameof(Index));
        }


        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var temAcesso = await UsuarioTemAcesso("Cancelar Venda", _context);

            if (!temAcesso)
            {
                ViewBag.TemAcesso = false;
                return RedirectToAction("Index", "Home");
            }

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

            //Deletar itemVenda
            var item_venda = _context.ItensVenda.Where(e => e.VendaId == id).OrderByDescending(e => e.VendaId);
            List<ItemVenda> itemVendas = item_venda.ToList();

            for (int i = 0; i < itemVendas.Count; i++)
            {
                _context.ItensVenda.Remove(itemVendas[i]);

                //Voltar quantidade do estoque                
                var id_estoque = _daoProdutoEstoque.RetornarIdEstoque(itemVendas[i].ProdutoId);
                var estoque = _context.Estoque.Find(id_estoque);
                estoque.Quantidade = estoque.Quantidade + itemVendas[i].QuantidadeProduto;
                _context.Estoque.Update(estoque);
            }

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
            //ViewBag.ListaVendas = _context.Venda.ToList();

            List<Venda> listaVendas = _context.Venda.ToList();

            List<VendaPdf> lista = new List<VendaPdf>();
            VendaPdf item;
            var cliente = "";
            var vendedor = "";
            foreach (var ls in listaVendas)
            {
                cliente = _context.Cliente.Where(e => e.Id == Convert.ToInt32(ls.ClienteId)).Select(e => e.Nome).FirstOrDefault();
                vendedor = _context.Vendedor.Where(e => e.Id == Convert.ToInt32(ls.VendedorId)).Select(e => e.Nome).FirstOrDefault();

                item = new VendaPdf
                {
                    Id = ls.Id,
                    Data = ls.Data,
                    NomeCliente = cliente,
                    NomeVendedor = vendedor,

                    Total = ls.Total,
                };
                lista.Add(item);
            }

            ViewBag.ListaVendas = lista;
        }

        private void CarregaListaItemVenda(int? id)
        {
            var item_venda = _context.ItensVenda.Where(e => e.VendaId == id).OrderByDescending(e => e.VendaId).Include(e => e.Produto);
            ViewBag.ItensVendas = item_venda.ToList();
        }

        private void CarregarDados()
        {
            List<Produto> listaProdutos = _daoVenda.RetornarListaProdutos();

            List<Produto> lista = new List<Produto>();
            Produto item;
            int id_estoque = 0;
            Estoque estoque;
            foreach (var ls in listaProdutos)
            {
                id_estoque = _daoProdutoEstoque.RetornarIdEstoque(ls.Id);
                estoque = _context.Estoque.Find(id_estoque);

                if (estoque.Quantidade > 0)
                {
                    item = new Produto
                    {
                        Id = ls.Id,
                        Nome = ls.Nome,
                        Descricao = ls.Descricao,
                        PrecoUnitario = ls.PrecoUnitario,
                        Quantidade = ls.Quantidade,
                        UnidadeMedida = ls.UnidadeMedida,
                        LinkFoto = ls.LinkFoto
                    };
                    lista.Add(item);
                }
            }

            ViewBag.ListaProdutos = lista;
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
