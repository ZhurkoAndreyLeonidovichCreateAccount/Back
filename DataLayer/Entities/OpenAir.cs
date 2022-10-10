using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("OpenAir")]
    public class OpenAir : CommonDataOfConcert
    {
       // public string? NameOfEvent { get; set; }
        public string? HowToGet { get; set; }
        public string? Headliner { get; set; }
    }
}
