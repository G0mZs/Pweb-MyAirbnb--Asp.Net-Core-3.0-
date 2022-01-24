using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PwebTP.Models;



namespace PwebTP.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            
        }
        

        public DbSet<Rooms> Rooms { get; set; }

        public DbSet<Reservations> Reservations { get; set; }

        public DbSet<Reviews> Reviews { get; set; }

        public DbSet<Media> Media { get; set; }

        public DbSet<Checklist> Checklist { get; set; }

        public DbSet<Procedures> Procedures { get; set; }

     
    }
}
