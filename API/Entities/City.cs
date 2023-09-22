using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Cities")]

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Decription { get; set; }
        public decimal Percentage { get; set; }
        public bool Deleted { get; set; }

    }
}