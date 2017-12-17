using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Model;

namespace WebManager.DBContexts
{
    public class DatabaseContext: DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Model.Group> Groups { get; set; }
        public virtual DbSet<Model.Task> Tasks { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Username).IsRequired();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.Id).IsRequired();
                entity.HasMany(e => e.UsersGroups)
                        .WithOne(o => o.User)
                        .HasForeignKey(k => k.UserId);
            });

            modelBuilder.Entity<Model.Group>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.GroupName).IsRequired();
                entity.HasMany(e => e.Tasks)
                        .WithOne(o => o.Group);
                entity.HasMany(e => e.UsersGroups)
                        .WithOne(o => o.Group)
                        .HasForeignKey(k => k.GroupId);
            });

            modelBuilder.Entity<Model.Task>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.CreationDate).IsRequired();
                entity.HasOne(e => e.Group)
                        .WithMany(o => o.Tasks)
                        .HasForeignKey(k => k.GroupId);
            });

            modelBuilder.Entity<UserGroup>()
                .HasKey(t => new { t.UserId, t.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UsersGroups)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.Group)
                .WithMany(c => c.UsersGroups)
                .HasForeignKey(bc => bc.GroupId);
        }
    }
}
