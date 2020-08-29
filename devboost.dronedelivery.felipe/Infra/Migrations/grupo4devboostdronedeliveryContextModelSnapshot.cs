﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using devboost.dronedelivery.felipe.EF.Data;

namespace devboost.dronedelivery.felipe.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class grupo4devboostdronedeliveryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("devboost.dronedelivery.felipe.DTO.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("devboost.dronedelivery.felipe.DTO.Models.Drone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Autonomia")
                        .HasColumnType("int");

                    b.Property<int>("Capacidade")
                        .HasColumnType("int");

                    b.Property<int>("Carga")
                        .HasColumnType("int");

                    b.Property<float>("Perfomance")
                        .HasColumnType("real");

                    b.Property<int>("Velocidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Drone");
                });

            modelBuilder.Entity("devboost.dronedelivery.felipe.DTO.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHoraFinalizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataHoraInclusao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Peso")
                        .HasColumnType("int");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("devboost.dronedelivery.felipe.DTO.Models.PedidoDrone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataHoraFinalizacao")
                        .HasColumnType("datetime2");

                    b.Property<double>("Distancia")
                        .HasColumnType("float");

                    b.Property<int>("DroneId")
                        .HasColumnType("int");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<int>("StatusEnvio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DroneId");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoDrones");
                });

            modelBuilder.Entity("devboost.dronedelivery.felipe.DTO.Models.Pedido", b =>
                {
                    b.HasOne("devboost.dronedelivery.felipe.DTO.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");
                });

            modelBuilder.Entity("devboost.dronedelivery.felipe.DTO.Models.PedidoDrone", b =>
                {
                    b.HasOne("devboost.dronedelivery.felipe.DTO.Models.Drone", "Drone")
                        .WithMany()
                        .HasForeignKey("DroneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("devboost.dronedelivery.felipe.DTO.Models.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
