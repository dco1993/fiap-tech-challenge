namespace Services.DTO
{
    public class DiscountDTO
    {
        public DateTime StartDiscount { get; set; }
        public DateTime EndDiscount { get; set; }
        public float DiscountPercentage { get; set; }
        public decimal DiscountedPrice { get; set; }
        public GameDTO Game { get; set; }
    }
}
