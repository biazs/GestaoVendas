namespace GestaoVendas.Models.Dao
{
    public class CarrinhoCompra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double PrecoUnitario { get; set; }
        public double Total { get; set; }

        public CarrinhoCompra()
        {
        }

        public CarrinhoCompra(int id, string nome, int quantidade, double precoUnitario)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            Total = quantidade * precoUnitario;
        }

    }
}
