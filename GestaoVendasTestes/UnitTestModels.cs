using GestaoVendas.Data;
using GestaoVendas.Models;
using GestaoVendas.Models.Dao;
using System.Collections.Generic;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoVendasTestes
{
    
    public class UnitTestModels
    {
        //[Fact]
        //public void CheckTypeListaProdutos()
        //{
        //    //DaoProduto daoProduto = new DaoProduto();
        //    //List<Produto> lista = daoProduto.ListarTodosProdutos();
        //    //Assert.IsType<List<Produto>>(lista);
        //}

        [Fact]
        public void ValidaQuantidadeProduto()
        {            
            int quantidade = 2;            
            Produto produto = new Produto();
            produto.Quantidade = quantidade;

            // Assert            
            Assert.Equal(quantidade, produto.Quantidade);
           
        }
    }
}
