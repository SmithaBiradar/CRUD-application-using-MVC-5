using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeApplication.Models
{
    public class EmployeeContext:DbContext
    {
       

        public System.Data.Entity.DbSet<EmployeeApplication.Models.Employee> Employees { get; set; }
    }
}