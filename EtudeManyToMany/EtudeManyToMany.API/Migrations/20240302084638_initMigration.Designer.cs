﻿// <auto-generated />
using EtudeManyToMany.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EtudeManyToMany.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240302084638_initMigration")]
    partial class initMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Administrateur", b =>
                {
                    b.Property<int>("AdministrateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdministrateurId"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdministrateurId");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            AdministrateurId = 1,
                            Nom = "Admin",
                            Password = "PAss62!"
                        });
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Conducteur", b =>
                {
                    b.Property<int>("ConducteurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConducteurId"), 1L, 1);

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("ConducteurId");

                    b.HasIndex("UtilisateurId")
                        .IsUnique();

                    b.ToTable("Conducteurs");

                    b.HasData(
                        new
                        {
                            ConducteurId = 1,
                            UtilisateurId = 1
                        },
                        new
                        {
                            ConducteurId = 2,
                            UtilisateurId = 2
                        });
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Passager", b =>
                {
                    b.Property<int>("PassagerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PassagerId"), 1L, 1);

                    b.Property<int>("UtilisateurId")
                        .HasColumnType("int");

                    b.HasKey("PassagerId");

                    b.HasIndex("UtilisateurId")
                        .IsUnique();

                    b.ToTable("Passagers");

                    b.HasData(
                        new
                        {
                            PassagerId = 1,
                            UtilisateurId = 1
                        },
                        new
                        {
                            PassagerId = 2,
                            UtilisateurId = 2
                        });
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"), 1L, 1);

                    b.Property<int>("PassagerId")
                        .HasColumnType("int");

                    b.Property<int>("TrajetId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("PassagerId");

                    b.HasIndex("TrajetId");

                    b.ToTable("Reservations");

                    b.HasData(
                        new
                        {
                            ReservationId = 1,
                            PassagerId = 1,
                            TrajetId = 1
                        },
                        new
                        {
                            ReservationId = 2,
                            PassagerId = 2,
                            TrajetId = 1
                        });
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Trajet", b =>
                {
                    b.Property<int>("TrajetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrajetId"), 1L, 1);

                    b.Property<int>("ConducteurId")
                        .HasColumnType("int");

                    b.Property<string>("LieuArrivee")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LieuDepart")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TrajetId");

                    b.HasIndex("ConducteurId");

                    b.ToTable("Trajets");

                    b.HasData(
                        new
                        {
                            TrajetId = 1,
                            ConducteurId = 2,
                            LieuArrivee = "Marseille",
                            LieuDepart = "Paris"
                        });
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Utilisateur", b =>
                {
                    b.Property<int>("UtilisateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UtilisateurId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UtilisateurId");

                    b.ToTable("Utilisateurs");

                    b.HasData(
                        new
                        {
                            UtilisateurId = 1,
                            Email = "jean.dupont@gmail.com",
                            Nom = "Dupont",
                            Phone = "0607080910"
                        },
                        new
                        {
                            UtilisateurId = 2,
                            Email = "pierre.dujardin@gmail.com",
                            Nom = "Dujardin",
                            Phone = "0607880910"
                        },
                        new
                        {
                            UtilisateurId = 3,
                            Email = "john.doe@gmail.com",
                            Nom = "Doe",
                            Phone = "0609090910"
                        });
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Conducteur", b =>
                {
                    b.HasOne("EtudeManyToMany.Core.Model.Utilisateur", "Utilisateur")
                        .WithOne("Conducteur")
                        .HasForeignKey("EtudeManyToMany.Core.Model.Conducteur", "UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Passager", b =>
                {
                    b.HasOne("EtudeManyToMany.Core.Model.Utilisateur", "Utilisateur")
                        .WithOne("Passager")
                        .HasForeignKey("EtudeManyToMany.Core.Model.Passager", "UtilisateurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilisateur");
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Reservation", b =>
                {
                    b.HasOne("EtudeManyToMany.Core.Model.Passager", "Passager")
                        .WithMany("Reservations")
                        .HasForeignKey("PassagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EtudeManyToMany.Core.Model.Trajet", "Trajet")
                        .WithMany("Reservations")
                        .HasForeignKey("TrajetId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Passager");

                    b.Navigation("Trajet");
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Trajet", b =>
                {
                    b.HasOne("EtudeManyToMany.Core.Model.Conducteur", "Conducteur")
                        .WithMany("Trajets")
                        .HasForeignKey("ConducteurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conducteur");
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Conducteur", b =>
                {
                    b.Navigation("Trajets");
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Passager", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Trajet", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("EtudeManyToMany.Core.Model.Utilisateur", b =>
                {
                    b.Navigation("Conducteur");

                    b.Navigation("Passager");
                });
#pragma warning restore 612, 618
        }
    }
}