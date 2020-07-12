using GestaoVendas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Vendas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Vendas(Relatorio relatorio)
        {
            if (relatorio.DataDe.Year == 1)
            {
                //ViewBag.ListaVendas = new Venda().ListagemVendas();
            }
            else
            {
                string DataDe = relatorio.DataDe.ToString("yyyy-MM-dd");
                string DataAte = relatorio.DataAte.ToString("yyyy-MM-dd");
                // ViewBag.ListaVendas = new Venda().ListagemVendas(DataDe, DataAte);
            }

            if (ViewBag.ListaVendas.Count == 0)
            {
                // TempData["MSG_E"] = Mensagem.MSG_E007;
            }

            return View();
        }

        /*public IActionResult Grafico()
        {
            List<GraficoProdutos> lista = new GraficoProdutos().RetornarGrafico();
            string valores = "";
            string labels = "";
            string cores = "";

            var random = new Random();

            //Percorre a lista de itens para compor o gráfico
            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].QtdeVendido.ToString() + ",";
                labels += "'" + lista[i].DescricaoProduto.ToString() + "',";

                //Escolher aleatoriamente as cores para compor o gráfico tipo torta
                cores += "'" + String.Format("#{0:X6}", random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }*/
    }
}
