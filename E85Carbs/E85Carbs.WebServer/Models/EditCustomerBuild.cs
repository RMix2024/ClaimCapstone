using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E85Carbs.WebServer.Data;

namespace E85Carbs.WebServer.Models
{
    public class EditCustomerBuild
    {
        public int Id { get; set; }

        public void EditAProduct(CustomerBuild customerBuild)
        {
            customerBuild.CustomerName = customerBuild.CustomerName;
            customerBuild.CustomerBuildTitle = customerBuild.CustomerBuildTitle;
            customerBuild.CustomerBuildDescription = customerBuild.CustomerBuildDescription;
        
        }
    }
}
