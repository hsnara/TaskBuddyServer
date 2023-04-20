namespace TaskBuddyServer.Data

{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using TaskBuddyClassLibrary.Models;

    public class TaskBuddyDbContext : DbContext
    {
        public DbSet<TaskBuddyTask> TaskBuddyTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=taskbuddy.db");
        public TaskBuddyDbContext(DbContextOptions<TaskBuddyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskBuddyTask>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }

    }


}
