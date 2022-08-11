using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Participation_Microservice.Models
{
    [Table("Participation")]
    public class Participation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Participation_id { get; set; }
        public int Player_id { get; set; }
        public string Player_name { get; set; }
        public int Event_id { get; set; }
        public string Event_name { get; set; }
        public int Sports_id { get; set; }
        public string Sports_name { get; set; }
        public string Status { get; set; }
    }
}
