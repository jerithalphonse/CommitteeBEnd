﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Helpers;

namespace WebApi.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Entities.MessagingModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("By");

                    b.Property<string>("CreatedAt");

                    b.Property<string>("Message");

                    b.Property<string>("To");

                    b.Property<string>("WilayatCode");

                    b.HasKey("Id");

                    b.HasIndex("By");

                    b.HasIndex("WilayatCode");

                    b.ToTable("Messaging");
                });

           

            modelBuilder.Entity("WebApi.Entities.MessagingModel", b =>
                {
                    b.HasOne("WebApi.Entities.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("By");

                    b.HasOne("WebApi.Entities.Wilayats", "Wilayat")
                        .WithMany()
                        .HasForeignKey("WilayatCode");
                });

#pragma warning restore 612, 618
        }
    }
}
