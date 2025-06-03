namespace Services.DTO
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Metacritic { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string About { get; set; }
        public decimal Price { get; set; }
        public DiscountDTO Discount { get; set; }
    }
}
