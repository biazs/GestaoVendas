using System;
using System.Collections.Generic;
using GestaoVendas.Libraries.Mensagem;
using GestaoVendas.Models;
using GestaoVendas.Models.Dao;
using GestaoVendas.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoVendas.Controllers
{
    public class RelatorioController : BaseController
    {
        private readonly DaoVenda _daoVenda;
        private readonly RelatorioService _relatorio;

        public RelatorioController(DaoVenda daoVenda, RelatorioService relatorio)
        {
            _daoVenda = daoVenda;
            _relatorio = relatorio;
        }
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
                ViewBag.ListaVendas = _daoVenda.ListagemVendas();
            }
            else
            {
                DateTime dataDe = relatorio.DataDe;
                DateTime dataAte = relatorio.DataAte.AddDays(1);
                ViewBag.ListaVendas = _daoVenda.ListagemVendas(dataDe, dataAte);
            }

            if (ViewBag.ListaVendas.Count == 0)
            {
                TempData["MSG_E"] = Mensagem.MSG_E007;
            }

            return View();
        }

        public IActionResult Grafico()
        {
            List<GraficoProdutos> lista = _relatorio.RetornarGrafico();
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
        }


        [HttpGet]
        public IActionResult VendasPorVendedor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VendasPorVendedor(int mes, int ano)
        {
            var lista = _relatorio.RetornarVendasPorVendedor(mes, ano);


            if (lista.Count == 0)
            {
                TempData["MSG_E"] = Mensagem.MSG_E007;
                return View();
            }

            string valores = "";
            string labels = "";
            string cores = "";


            var random = new Random();

            //Percorre a lista de itens para compor o gráfico
            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].QtdeVendido.ToString() + ",";
                labels += "'" + lista[i].Vendedor.ToString() + "',";

                //Escolher aleatoriamente as cores para compor o gráfico tipo torta
                cores += "'" + String.Format("#{0:X6}", random.Next(0x1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }

    }
}
