using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyHomepage.Models;

namespace MyHomepage.Data
{
    public class MyHomepageContext : DbContext
    {
        public MyHomepageContext (DbContextOptions<MyHomepageContext> options)
            : base(options)
        {
        }

        public DbSet<MyHomepage.Models.Movie>? Movie { get; set; }
    }
}
