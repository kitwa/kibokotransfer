using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class SearchTransactionDto
    {
        public string SenderName { get; set; }
        public int SenderCityId { get; set; }
        public string RecipientName { get; set; }
        public int RecipientCityId { get; set; }
        public string Code { get; set; }
        public int StatusId { get; set; }
        // public int CityId { get; set; }
        public DateTime Created { get; set; } 

    }
}
