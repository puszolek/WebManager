using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Model;

namespace WebManager.DBContexts
{
    public class UsersContext: DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Groups).IsRequired();
                entity.Property(e => e.Id).IsRequired();
            });
        }
    }
}
