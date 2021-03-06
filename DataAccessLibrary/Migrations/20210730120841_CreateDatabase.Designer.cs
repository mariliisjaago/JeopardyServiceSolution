// <auto-generated />
using System;
using DataAccessLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLibrary.Migrations
{
    [DbContext(typeof(JeopardyContext))]
    [Migration("20210730120841_CreateDatabase")]
    partial class CreateDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccessLibrary.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataAccessLibrary.Models.QuestionAndAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<Guid?>("RoundId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ShowDataId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RoundId");

                    b.HasIndex("ShowDataId");

                    b.HasIndex("Value");

                    b.ToTable("QuestionsAndAnswers");
                });

            modelBuilder.Entity("DataAccessLibrary.Models.Round", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoundName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("DataAccessLibrary.Models.Show", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AirDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShowNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AirDate");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("DataAccessLibrary.Models.QuestionAndAnswer", b =>
                {
                    b.HasOne("DataAccessLibrary.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("DataAccessLibrary.Models.Round", "Round")
                        .WithMany()
                        .HasForeignKey("RoundId");

                    b.HasOne("DataAccessLibrary.Models.Show", "ShowData")
                        .WithMany()
                        .HasForeignKey("ShowDataId");

                    b.Navigation("Category");

                    b.Navigation("Round");

                    b.Navigation("ShowData");
                });
#pragma warning restore 612, 618
        }
    }
}
