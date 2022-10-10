using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class CommonDataOfConcert
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }
        public string? NamePerformer { get; set; }
        public DateTime DateConcert { get; set; }
        public string? LocationConcert { get; set; }
        public int AmountOfTickets { get; set; }
        public string? Image { get; set; }

        //Навигационное свойства
        public List<Ticket>? Tickets { get; set; }
    }
}
