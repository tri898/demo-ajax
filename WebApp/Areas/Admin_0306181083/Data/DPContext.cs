using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin_0306181083.Models;

namespace WebApp.Areas.Admin_0306181083.Data
{
    public class DPContext : DbContext
    { 
        public DPContext(DbContextOptions<DPContext> options)
            : base(options)
        {

        }
        public DbSet<Category> Category{ get; set; }
        public DbSet<Post> Post { get; set; }

    }
}
