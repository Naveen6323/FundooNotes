using DataAcessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.Context
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions options):base(options) {

        }
        public DbSet<User> Users { get; set; }

    }
}
