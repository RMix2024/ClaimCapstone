using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E85Carbs.WebServer.Data;
using E85Carbs.WebServer.Models;
using Microsoft.Extensions.Logging;

namespace E85Carbs.WebServer.Services
{
    public class CustomerBuildsService
    {

        readonly ApplicationDbContext _context;
        readonly ILogger _logger;

        public CustomerBuildsService(ApplicationDbContext context, ILoggerFactory factory)
        {
            this._context = context;
            this._logger = factory.CreateLogger<CustomerBuildsService>();
        }

        public CustomerBuild AddNewCustomerBuild(AddCustomerBuild name)
        {
            var customerBuild = name.ToCustomerBuild();
            _context.Add(customerBuild);
            _context.SaveChanges();
            return customerBuild;
        }

        public List<CustomerBuild> GetAllCustomerBuilds()
        {
            var customerBuildsList = _context.customerBuilds.ToList();
            return customerBuildsList;
        }

        public CustomerBuild GetACustomerBuild(int? id)
        {
            return this._context.customerBuilds.Where(x => x.CustomerBuildId == id).FirstOrDefault();
        }

   

        public void UpdateCustomerBuild(int customerBuildId, string customerName, string customerBuilddescription, string customerBuildtitle)
        {
            var updateCustomerBuild = _context.customerBuilds.Find(customerBuildId);
            updateCustomerBuild.CustomerName = customerName;
            updateCustomerBuild.CustomerBuildDescription = customerBuilddescription;
            updateCustomerBuild.CustomerBuildTitle = customerBuildtitle;
            _context.SaveChanges();

        }
    }
}
