﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaA01.Data;

#nullable disable

namespace TiendaA01.Migrations
{
    [DbContext(typeof(TiendaA01Context))]
    [Migration("20230819085406_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TiendaA01.Models.Componente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Calor")
                        .HasColumnType("int");

                    b.Property<int>("Cores")
                        .HasColumnType("int");

                    b.Property<float>("Coste")
                        .HasColumnType("real");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Megas")
                        .HasColumnType("bigint");

                    b.Property<int>("OrdenadorId")
                        .HasColumnType("int");

                    b.Property<string>("Serie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoComponente")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrdenadorId");

                    b.ToTable("Componente");
                });

            modelBuilder.Entity("TiendaA01.Models.Ordenador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("Ordenador");
                });

            modelBuilder.Entity("TiendaA01.Models.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Cliente")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("NombrePedido")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Pedido");
                });

            modelBuilder.Entity("TiendaA01.Models.Componente", b =>
                {
                    b.HasOne("TiendaA01.Models.Ordenador", "Ordenador")
                        .WithMany("Componentes")
                        .HasForeignKey("OrdenadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ordenador");
                });

            modelBuilder.Entity("TiendaA01.Models.Ordenador", b =>
                {
                    b.HasOne("TiendaA01.Models.Pedido", "Pedido")
                        .WithMany("Ordenadores")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("TiendaA01.Models.Ordenador", b =>
                {
                    b.Navigation("Componentes");
                });

            modelBuilder.Entity("TiendaA01.Models.Pedido", b =>
                {
                    b.Navigation("Ordenadores");
                });
#pragma warning restore 612, 618
        }
    }
}
