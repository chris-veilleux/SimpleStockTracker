namespace SimpleStockTracker.Models
{
    public class Holding
    {
        public int HoldingId { get; set; }
        public string? Ticker { get; set; }
        public DateTime TradeDate { get; set; }
        public string? TradeType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string? Account { get; set; }
    }
}
