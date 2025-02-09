﻿// <auto-generated />
using System;
using AutoDice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoDice.Migrations
{
    [DbContext(typeof(AutoDiceDbContext))]
    partial class AutoDiceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("AutoDice.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Experience")
                        .HasColumnType("REAL");

                    b.Property<double>("ExperienceCanBeSpent")
                        .HasColumnType("REAL");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("AutoDice.Models.CharacterHealth", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HealthTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("MaxValue")
                        .HasColumnType("REAL");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("CharacterId", "HealthTypeId");

                    b.HasIndex("HealthTypeId");

                    b.ToTable("CharacterHealths");
                });

            modelBuilder.Entity("AutoDice.Models.CharacterInventory", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Count")
                        .HasColumnType("INTEGER");

                    b.HasKey("CharacterId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("CharacterInventories");
                });

            modelBuilder.Entity("AutoDice.Models.CharacterSkill", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("CharacterId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("CharacterSkills");
                });

            modelBuilder.Entity("AutoDice.Models.HealthType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("CanBeLost")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("PassiveDecrease")
                        .HasColumnType("REAL");

                    b.Property<double>("PassiveIncrease")
                        .HasColumnType("REAL");

                    b.Property<double>("PowerWeight")
                        .HasColumnType("REAL");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("HealthTypes");
                });

            modelBuilder.Entity("AutoDice.Models.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagePath")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("AutoDice.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsGameMaster")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("AutoDice.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("AutoDice.Models.SkillTree", b =>
                {
                    b.Property<int>("ParentSkillId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubskillId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Weight")
                        .HasColumnType("REAL");

                    b.HasKey("ParentSkillId", "SubskillId");

                    b.HasIndex("SubskillId");

                    b.ToTable("SkillTrees");
                });

            modelBuilder.Entity("AutoDice.Models.TgUser", b =>
                {
                    b.Property<long>("TgUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TgUserId");

                    b.HasIndex("PlayerId");

                    b.ToTable("TgUser");
                });

            modelBuilder.Entity("AutoDice.Models.WebUserSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("WebUserSession");
                });

            modelBuilder.Entity("AutoDice.Models.Character", b =>
                {
                    b.HasOne("AutoDice.Models.Player", "Player")
                        .WithMany("Characters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("AutoDice.Models.CharacterHealth", b =>
                {
                    b.HasOne("AutoDice.Models.Character", "Character")
                        .WithMany("CharacterHealths")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoDice.Models.HealthType", "HealthType")
                        .WithMany("CharacterHealths")
                        .HasForeignKey("HealthTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("HealthType");
                });

            modelBuilder.Entity("AutoDice.Models.CharacterInventory", b =>
                {
                    b.HasOne("AutoDice.Models.Character", "Character")
                        .WithMany("CharacterInventories")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoDice.Models.InventoryItem", "Item")
                        .WithMany("CharacterInventories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("AutoDice.Models.CharacterSkill", b =>
                {
                    b.HasOne("AutoDice.Models.Character", "Character")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoDice.Models.Skill", "Skill")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Skill");
                });

            modelBuilder.Entity("AutoDice.Models.SkillTree", b =>
                {
                    b.HasOne("AutoDice.Models.Skill", "ParentSkill")
                        .WithMany("ParentSkillLinks")
                        .HasForeignKey("ParentSkillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AutoDice.Models.Skill", "Subskill")
                        .WithMany("SubskillLinks")
                        .HasForeignKey("SubskillId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ParentSkill");

                    b.Navigation("Subskill");
                });

            modelBuilder.Entity("AutoDice.Models.TgUser", b =>
                {
                    b.HasOne("AutoDice.Models.Player", "Player")
                        .WithMany("TgUsers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("AutoDice.Models.WebUserSession", b =>
                {
                    b.HasOne("AutoDice.Models.Player", "Player")
                        .WithMany("WebUserSessions")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("AutoDice.Models.Character", b =>
                {
                    b.Navigation("CharacterHealths");

                    b.Navigation("CharacterInventories");

                    b.Navigation("CharacterSkills");
                });

            modelBuilder.Entity("AutoDice.Models.HealthType", b =>
                {
                    b.Navigation("CharacterHealths");
                });

            modelBuilder.Entity("AutoDice.Models.InventoryItem", b =>
                {
                    b.Navigation("CharacterInventories");
                });

            modelBuilder.Entity("AutoDice.Models.Player", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("TgUsers");

                    b.Navigation("WebUserSessions");
                });

            modelBuilder.Entity("AutoDice.Models.Skill", b =>
                {
                    b.Navigation("CharacterSkills");

                    b.Navigation("ParentSkillLinks");

                    b.Navigation("SubskillLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
