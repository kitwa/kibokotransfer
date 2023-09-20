using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class PropertyUpdateDto
    {
        // public int Id { get; set; }
        public int Price { get; set; }
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Garage { get; set; }
        public string PropertyType { get; set; }
        public string Description { get; set; } 
        public string City { get; set; }
        public string Country { get; set; }
        public string YoutubeLink { get; set; }

    }
}
