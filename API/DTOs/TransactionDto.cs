using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class TransactionDto
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
        public int StatusId { get; set; }
        public Status Status  { get; set; }
        public MemberDto appUser {get; set; }
        public bool Deleted { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
