﻿// <auto-generated />
using System;
using BBTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BBTracker.Persistence.Migrations
{
    [DbContext(typeof(BBTrackerContext))]
    partial class BBTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BBTracker.Model.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("End")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ScoreA")
                        .HasColumnType("int");

                    b.Property<int>("ScoreB")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOutside")
                        .HasColumnType("bit");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Play", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("GameTime")
                        .HasColumnType("time");

                    b.Property<bool>("IsTeamB")
                        .HasColumnType("bit");

                    b.Property<int>("PlayType")
                        .HasColumnType("int");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Plays");

                    b.HasDiscriminator<int>("PlayType");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Nick")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BBTracker.Model.Models.PlayerGame", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("TeamB")
                        .HasColumnType("bit");

                    b.HasKey("PlayerId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("PlayerGames");
                });

            modelBuilder.Entity("BBTracker.Model.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId")
                        .IsUnique()
                        .HasFilter("[PlayerId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Assist", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.Property<Guid>("FieldGoalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("FieldGoalId");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Block", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.Property<Guid>("FieldGoalBlockedId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("FieldGoalBlockedId");
                });

            modelBuilder.Entity("BBTracker.Model.Models.FieldGoal", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.Property<bool>("Made")
                        .HasColumnType("bit");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<bool>("WasAssisted")
                        .HasColumnType("bit");

                    b.Property<bool>("WasBlocked")
                        .HasColumnType("bit");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Foul", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Rebound", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.Property<Guid>("FieldGoalReboundedId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsOffensive")
                        .HasColumnType("bit");

                    b.HasIndex("FieldGoalReboundedId");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Steal", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Substitution", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.Property<bool>("SubbedIn")
                        .HasColumnType("bit");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Turnover", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(6);
                });

            modelBuilder.Entity("BBTracker.Model.Models.Game", b =>
                {
                    b.HasOne("BBTracker.Model.Models.User", "Owner")
                        .WithMany("OwnedGames")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Play", b =>
                {
                    b.HasOne("BBTracker.Model.Models.Game", "Game")
                        .WithMany("Plays")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBTracker.Model.Models.Player", "Player")
                        .WithMany("Plays")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BBTracker.Model.Models.PlayerGame", b =>
                {
                    b.HasOne("BBTracker.Model.Models.Game", "Game")
                        .WithMany("PlayerGames")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BBTracker.Model.Models.Player", "Player")
                        .WithMany("PlayerGames")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BBTracker.Model.Models.User", b =>
                {
                    b.HasOne("BBTracker.Model.Models.Player", "Player")
                        .WithOne("User")
                        .HasForeignKey("BBTracker.Model.Models.User", "PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Assist", b =>
                {
                    b.HasOne("BBTracker.Model.Models.FieldGoal", "FieldGoal")
                        .WithMany()
                        .HasForeignKey("FieldGoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldGoal");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Block", b =>
                {
                    b.HasOne("BBTracker.Model.Models.FieldGoal", "FieldGoalBlocked")
                        .WithMany()
                        .HasForeignKey("FieldGoalBlockedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldGoalBlocked");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Rebound", b =>
                {
                    b.HasOne("BBTracker.Model.Models.FieldGoal", "FieldGoalRebounded")
                        .WithMany()
                        .HasForeignKey("FieldGoalReboundedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldGoalRebounded");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Game", b =>
                {
                    b.Navigation("PlayerGames");

                    b.Navigation("Plays");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Player", b =>
                {
                    b.Navigation("PlayerGames");

                    b.Navigation("Plays");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BBTracker.Model.Models.User", b =>
                {
                    b.Navigation("OwnedGames");
                });
#pragma warning restore 612, 618
        }
    }
}
