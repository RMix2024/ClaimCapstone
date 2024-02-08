﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace E85Carbs.WebServer.Data
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
