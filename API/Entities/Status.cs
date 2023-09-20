using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Statuses")]

    public class Status
    {
        public int Id { get; set; }
        public int Identifier { get; set; }
        public string Value { get; set; }

    }
}