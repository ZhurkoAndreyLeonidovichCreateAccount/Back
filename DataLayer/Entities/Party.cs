using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities
{
    [Table("Party")]
    public class Party : CommonDataOfConcert
    {
        public int Age { get; set; }
    }
}
