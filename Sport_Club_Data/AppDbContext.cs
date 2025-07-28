using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sport_Club_Data.Entitys;
namespace Sport_Club_Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserAccounts> UserAccounts { get; set; }
        public DbSet<Persons> Persons { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<BeltRanks> BeltRankss { get; set; }
        public DbSet<BeltTests> BeltTests { get; set; }
        public DbSet<SubscriptionPeriods> SubscriptionPeriods { get; set; }
        public DbSet<MemberInstructors> MemberInstructors { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Payments> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(x => x.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
