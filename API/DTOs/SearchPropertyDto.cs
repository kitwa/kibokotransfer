using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class SearchPropertyDto
    {
        // public int Id { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public string PropertyType { get; set; }
        public string City { get; set; }


    }
}
