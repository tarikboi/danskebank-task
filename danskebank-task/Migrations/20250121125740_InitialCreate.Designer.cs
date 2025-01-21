﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace danskebank_task.Migrations
{
    [DbContext(typeof(TaxDbContext))]
    [Migration("20250121125740_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("danskebank_task.Models.Tax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Municipality")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("TaxRate")
                        .HasColumnType("REAL");

                    b.Property<int>("TaxType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Taxes");
                });
#pragma warning restore 612, 618
        }
    }
}
