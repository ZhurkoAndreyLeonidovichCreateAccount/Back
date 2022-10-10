using DataLayer.Entities;

namespace WebApiConcerts.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
