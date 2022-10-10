namespace WebApiConcerts.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Sector { get; set; }
        public bool booked { get; set; }
        public int ConcertId { get; set; }
        public string? UserId { get; set; }
    }
}
