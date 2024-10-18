﻿// <auto-generated />
using System;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("DomainLayer.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Jordan"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Egypt"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Syria"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Palestine"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Saudi arabia"
                        },
                        new
                        {
                            Id = 6,
                            Name = "United Arab Emirates"
                        },
                        new
                        {
                            Id = 7,
                            Name = "kuwait"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Oman"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Bahrain"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Lebanon"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Yemen"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Iraq"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Qatar"
                        });
                });

            modelBuilder.Entity("DomainLayer.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Management"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Auditor"
                        });
                });

            modelBuilder.Entity("DomainLayer.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@happywarehouse.com",
                            IsActive = true,
                            Name = "Hamad Jafar",
                            PasswordHash = "$2a$11$R/ZmEsuyNKVaGNnXlDh6W.jshT./aA3QYPgnqlghm75lj9HpbKZXy",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("DomainLayer.Entities.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Warehouse", (string)null);
                });

            modelBuilder.Entity("DomainLayer.Entities.WarehouseItems", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("MsrpPrice")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SkuCode")
                        .HasColumnType("TEXT");

                    b.Property<int>("WareHouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemName")
                        .IsUnique();

                    b.HasIndex("WareHouseId");

                    b.ToTable("WarehouseItems", (string)null);
                });

            modelBuilder.Entity("DomainLayer.Entities.User", b =>
                {
                    b.HasOne("DomainLayer.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DomainLayer.Entities.WarehouseItems", b =>
                {
                    b.HasOne("DomainLayer.Entities.Warehouse", "WareHouse")
                        .WithMany("Items")
                        .HasForeignKey("WareHouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WareHouse");
                });

            modelBuilder.Entity("DomainLayer.Entities.Warehouse", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
