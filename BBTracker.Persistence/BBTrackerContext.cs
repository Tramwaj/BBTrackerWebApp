using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using BBTracker.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BBTracker.Model.Models;
using System.IO;

namespace BBTracker.Persistence
{
    public class BBTrackerContext : DbContext
    {
        private readonly string _connectionString;
        //public static readonly ILoggerFactory Logger = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public BBTrackerContext()
        {
            _connectionString = File.ReadAllText("connStr.txt");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        public DbSet<Play> Plays { get; set; }
        //public DbSet<Assist> Assists { get; set; }
        //public DbSet<FieldGoal> FieldGoals { get; set; }
        //public DbSet<Rebound> Rebounds { get; set; }
        //public DbSet<Substitution> Substitutions { get; set; }
        //public DbSet<Block> Blocks { get; set; }

    }
}
