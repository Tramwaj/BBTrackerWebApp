﻿// <auto-generated />
using System;
using BBTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BBTracker.Persistence.Migrations
{
    [DbContext(typeof(BBTrackerContext))]
    [Migration("20210218134820_BatchMigration")]
    partial class BatchMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
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

            modelBuilder.Entity("BBTracker.Model.Models.Play", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("GameTime")
                        .HasColumnType("time");

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

                    b.HasDiscriminator<int>("PlayType").HasValue(8);
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

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Assist", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("BBTracker.Model.Models.Block", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("BBTracker.Model.Models.FieldGoal", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("BBTracker.Model.Models.Foul", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(7);
                });

            modelBuilder.Entity("BBTracker.Model.Models.Rebound", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("BBTracker.Model.Models.Steal", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("BBTracker.Model.Models.Substitution", b =>
                {
                    b.HasBaseType("BBTracker.Model.Models.Play");

                    b.HasDiscriminator().HasValue(5);
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

            modelBuilder.Entity("BBTracker.Model.Models.Game", b =>
                {
                    b.Navigation("PlayerGames");

                    b.Navigation("Plays");
                });

            modelBuilder.Entity("BBTracker.Model.Models.Player", b =>
                {
                    b.Navigation("PlayerGames");

                    b.Navigation("Plays");
                });

            modelBuilder.Entity("BBTracker.Model.Models.User", b =>
                {
                    b.Navigation("OwnedGames");
                });
#pragma warning restore 612, 618
        }
    }
}
