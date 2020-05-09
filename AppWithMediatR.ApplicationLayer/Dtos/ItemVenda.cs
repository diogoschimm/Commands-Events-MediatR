namespace AppWithMediatR.ApplicationLayer.Dtos
{
    public class ItemVenda
    {
        public int IdProduto { get; set; }
        public int Qtd { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Desconto { get; set; }
    }
}
