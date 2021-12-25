using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvrakYonetimSistemi.Models;

namespace EvrakYonetimSistemi.Data
{


    public class ApplicationDbContext : IdentityDbContext<
      User,
      IdentityRole<string>,
      string,
      IdentityUserClaim<string>,
      IdentityUserRole<string>,
      IdentityUserLogin<string>,
      IdentityRoleClaim<string>,
      IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EvrakTipi> EvrakTipis { get; set; }
        public DbSet<Evrak> Evraks { get; set; }
        public DbSet<User> Admins { get; set; }
    }
}
