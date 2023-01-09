using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soccer2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.HomeTeam)
                .WithMany(x => x.HomeGames)
                .HasForeignKey(x => x.HomeTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<Game>()
                .HasOne(x => x.AwayTeam)
                .WithMany(x => x.AwayGames)
                .HasForeignKey(x => x.AwayTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<GameInfo> GamesInfo { get; set; }

        public DbSet<BetInfo> BetInfo { get; set; }

    }
}
