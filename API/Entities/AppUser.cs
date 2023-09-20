using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public int Phone { get; set; }
        public string Gender { get; set; }
        public int CityId { get; set; }
        public string Country { get; set; }
        public ICollection<Property> Properties {get; set; }
        public ICollection<Message> MessagesSent {get; set; }
        public ICollection<Message> MessagesReceived {get; set; }
        public ICollection<Transaction> Transactions {get; set; }
        public ICollection<AppUserRole> UserRoles { get; set; }
        public bool Deleted { get; set; }

    }
}