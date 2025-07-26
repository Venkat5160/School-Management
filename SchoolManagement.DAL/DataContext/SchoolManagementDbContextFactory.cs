using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.DAL.DataContext
{
    public class SchoolManagementDbContextFactory : IDesignTimeDbContextFactory<SchoolManagementDbContext>
    {
        public SchoolManagementDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SchoolManagementDbContext>();
            optionsBuilder.UseSqlServer("Data Source=WINDOWS-JQ14JQB;Initial Catalog=SchoolManagementDb;Integrated Security=True;Encrypt=False");

            return new SchoolManagementDbContext(optionsBuilder.Options);
        }
    }

}
