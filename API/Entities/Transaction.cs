using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Transactions")]

    public class Transaction
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public decimal SentAmount { get; set; }
        public decimal Profit { get; set; }
        public decimal TotalAmount { get; set; }
        public string SenderName { get; set; }
        public int SenderCityId { get; set; }
        public string RecipientName { get; set; }
        public int RecipientCityId { get; set; }
        public string Code { get; set; }
        public Status Status  { get; set; }
        public int StatusId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public AppUser appUser  { get; set; }
        public bool Deleted { get; set; }

    }
}