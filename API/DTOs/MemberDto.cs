using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public int Phone { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // public ICollection<PropertyDto> Properties {get; set; }
    }
}