﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Participation_Microservice.DBContext;

namespace Participation_Microservice.Migrations
{
    [DbContext(typeof(ParticipationContext))]
    [Migration("20220811054415_Participation")]
    partial class Participation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Participation_Microservice.Models.Participation", b =>
                {
                    b.Property<int>("Participation_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Event_id")
                        .HasColumnType("int");

                    b.Property<string>("Event_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Player_id")
                        .HasColumnType("int");

                    b.Property<string>("Player_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sports_id")
                        .HasColumnType("int");

                    b.Property<string>("Sports_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Participation_id");

                    b.ToTable("Participation");
                });
#pragma warning restore 612, 618
        }
    }
}
