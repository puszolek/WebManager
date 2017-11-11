using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebManager.Model;

namespace WebManager.DBContexts
{
    public class TasksContext: DbContext
    {
        public virtual DbSet<Model.Task> Tasks { get; set; }

        public TasksContext(DbContextOptions<TasksContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Task>(entity =>
            {
                entity.Property(e => e.Id).IsRequired();
                entity.Property(e => e.Group).IsRequired();
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.CreationDate).IsRequired();
            });
        }
    }
}
