﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTaskWebApp1;

namespace TestTaskWebApp1.Migrations
{
    [DbContext(typeof(DbEntities))]
    [Migration("20210118225400_Currencies_Added")]
    partial class Currencies_Added
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("TestTaskWebApp1.Models.Currency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("TestTaskWebApp1.Models.EchangeRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("FromAmmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("FromCurrency")
                        .HasColumnType("smallint");

                    b.Property<decimal>("ToAmmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("ToCurrency")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("EchangeRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
