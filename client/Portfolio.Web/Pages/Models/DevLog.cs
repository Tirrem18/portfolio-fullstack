using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;

namespace Portfolio.Web.Models
{
    [Table("devlogs")]
    public class DevLog : BaseModel
    {
        [PrimaryKey("id", false)]
        public Guid Id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("summary")]
        public string Summary { get; set; }

        [Column("desc")]
        public string Description { get; set; }

        [Column("tags")]
        public List<string> Tags { get; set; }
    }
}
