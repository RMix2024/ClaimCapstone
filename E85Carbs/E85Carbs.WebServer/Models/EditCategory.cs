using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E85Carbs.WebServer.Data;

namespace E85Carbs.WebServer.Models
{
    public class EditCategory
    {
        public int Id { get; set; }
        public void EditACategory(Category category)
        {
            category.CategoryID = category.CategoryID;
            category.CategoryName = category.CategoryName;
        }

    }
}
