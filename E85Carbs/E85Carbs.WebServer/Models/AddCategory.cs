using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E85Carbs.WebServer.Data;

namespace E85Carbs.WebServer.Models
{
    public class AddCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public Category ToCategory()
        {
            return new Category
            {
                CategoryID = CategoryID,
                CategoryName = CategoryName
            };
        }
    }
}
