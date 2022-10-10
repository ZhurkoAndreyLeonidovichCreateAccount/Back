namespace WebApiConcerts.Models
{
    public class CommonTypeData
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public string? NamePerformer { get; set; }
        public DateTime DateConcert { get; set; }
        public string? LocationConcert { get; set; }
        public int AmountOfTickets { get; set; }
        public IFormFile Image { get; set; }

        //Party
        public int Age { get; set; }

        //Classic
        public string? NameOfConcert { get; set; }
        public string? Voicetype { get; set; }

        //OpenAir
        public string? HowToGet { get; set; }
        public string? Headliner { get; set; }
    }
}
