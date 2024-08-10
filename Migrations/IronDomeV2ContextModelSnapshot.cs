﻿// <auto-generated />
using System;
using IronDomeV2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IronDomeV2.Migrations
{
    [DbContext(typeof(IronDomeV2Context))]
    partial class IronDomeV2ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IronDomeV2.Models.Attacker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Distance")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Attacker");
                });

            modelBuilder.Entity("IronDomeV2.Models.Countermeasure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DefenderId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LaunchTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("MethodOfAttackId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Range")
                        .HasColumnType("float");

                    b.Property<double>("Velocity")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DefenderId");

                    b.HasIndex("MethodOfAttackId")
                        .IsUnique();

                    b.ToTable("Countermeasure");
                });

            modelBuilder.Entity("IronDomeV2.Models.Defender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Defender");
                });

            modelBuilder.Entity("IronDomeV2.Models.MethodOfAttack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Range")
                        .HasColumnType("float");

                    b.Property<DateTime?>("TimeOfLaunch")
                        .HasColumnType("datetime2");

                    b.Property<double>("Velocity")
                        .HasColumnType("float");

                    b.Property<int>("VolleyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VolleyId");

                    b.ToTable("MethodOfAttack");
                });

            modelBuilder.Entity("IronDomeV2.Models.Volley", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttackerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AttackerId");

                    b.ToTable("Volley");
                });

            modelBuilder.Entity("IronDomeV2.Models.Countermeasure", b =>
                {
                    b.HasOne("IronDomeV2.Models.Defender", "Defender")
                        .WithMany("Countermeasures")
                        .HasForeignKey("DefenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IronDomeV2.Models.MethodOfAttack", "MethodOfAttack")
                        .WithOne("Countermeasure")
                        .HasForeignKey("IronDomeV2.Models.Countermeasure", "MethodOfAttackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Defender");

                    b.Navigation("MethodOfAttack");
                });

            modelBuilder.Entity("IronDomeV2.Models.MethodOfAttack", b =>
                {
                    b.HasOne("IronDomeV2.Models.Volley", "Volley")
                        .WithMany("MethodsOfAttack")
                        .HasForeignKey("VolleyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Volley");
                });

            modelBuilder.Entity("IronDomeV2.Models.Volley", b =>
                {
                    b.HasOne("IronDomeV2.Models.Attacker", "Attacker")
                        .WithMany("Volleys")
                        .HasForeignKey("AttackerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attacker");
                });

            modelBuilder.Entity("IronDomeV2.Models.Attacker", b =>
                {
                    b.Navigation("Volleys");
                });

            modelBuilder.Entity("IronDomeV2.Models.Defender", b =>
                {
                    b.Navigation("Countermeasures");
                });

            modelBuilder.Entity("IronDomeV2.Models.MethodOfAttack", b =>
                {
                    b.Navigation("Countermeasure")
                        .IsRequired();
                });

            modelBuilder.Entity("IronDomeV2.Models.Volley", b =>
                {
                    b.Navigation("MethodsOfAttack");
                });
#pragma warning restore 612, 618
        }
    }
}
