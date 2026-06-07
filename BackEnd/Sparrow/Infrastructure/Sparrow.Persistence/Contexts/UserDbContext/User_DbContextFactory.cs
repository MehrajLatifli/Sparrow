using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sparrow.Persistence.ServiceExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparrow.Persistence.Contexts.UserDbContext
{
    public class User_DbContextFactory : IDesignTimeDbContextFactory<User_DbContext>
    {
        public User_DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<User_DbContext>();
            optionsBuilder.UseSqlServer(ServiceExtension.UserDbConnectionString);

            return new User_DbContext (optionsBuilder.Options);
        }
    }
}
