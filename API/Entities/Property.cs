using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Properties")]
    public class Property
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public int Price { get; set; }
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Garage { get; set; }
        public string PropertyType { get; set; }
        public string Description { get; set; } 
        public string YoutubeLink { get; set; }
        public string MapUrl { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool sold { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public AppUser appUser  { get; set; }
        public int AppUserId { get; set; }

    }
} 