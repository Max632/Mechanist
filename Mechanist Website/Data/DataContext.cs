using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using Mechanist_Website.Models;

namespace Mechanist_Website.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<MembersInfo> Members { get; set; }
        public DbSet<ProjectInfo> Projects { get; set; }
        public DbSet<DownloadInfo> Downloads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MembersInfo>().ToTable("Member");
            modelBuilder.Entity<ProjectInfo>().ToTable("Project");
            modelBuilder.Entity<DownloadInfo>().ToTable("Download");
        }
    }
}
