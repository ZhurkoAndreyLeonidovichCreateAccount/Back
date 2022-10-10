using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table ("Сlassic")]
    public class Classic : CommonDataOfConcert
    {
       // public string? NameOfConcert { get; set; }
        public string? Voicetype { get; set; }
    }
}
