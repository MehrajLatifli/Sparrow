using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sparrow.Persistence.ServiceExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sparrow.Persistence.Contexts.MusicDbContext
{
    public class Music_DbContextFactory : IDesignTimeDbContextFactory<Music_DbContext>
    {
        public Music_DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Music_DbContext>();
            optionsBuilder.UseSqlServer(ServiceExtension.MusicDbConnectionString);

            return new Music_DbContext(optionsBuilder.Options);
        }
    }
}
