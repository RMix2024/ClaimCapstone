using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E85Carbs.WebServer.Data
{
    public class Make
    {
        [Key]
        public int MakeId { get; set; }

        [Required]
        public string MakeName { get; set; }
    }
}
