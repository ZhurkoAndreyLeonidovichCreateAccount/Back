using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Ticket
    {
       
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Sector { get; set; }
        public bool booked { get; set; } 

        //Навигационное свойства
        public CommonDataOfConcert? Concert { get; set; }
        public int ConcertId { get; set; }

        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }

    }
}
