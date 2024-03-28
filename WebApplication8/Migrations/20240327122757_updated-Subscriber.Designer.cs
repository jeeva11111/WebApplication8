﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication8.Data;

#nullable disable

namespace WebApplication8.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240327122757_updated-Subscriber")]
    partial class updatedSubscriber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication8.Models.Account.Profile.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AudioCount")
                        .HasColumnType("int");

                    b.Property<int>("Subscribers")
                        .HasColumnType("int");

                    b.Property<int>("VideoCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("WebApplication8.Models.Notes.Notes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("starred")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("WebApplication8.Models.Notify.Notify", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChennelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notifys");
                });

            modelBuilder.Entity("WebApplication8.Models.Quiz.DepOptionsList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("QuizId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("DepOptionsLists");
                });

            modelBuilder.Entity("WebApplication8.Models.Quiz.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Quiz");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.Chennel", b =>
                {
                    b.Property<int>("ChennelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChennelId"));

                    b.Property<byte[]>("BannerData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("BannerPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categorey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChennelDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChennelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChennelId");

                    b.HasIndex("UserId");

                    b.ToTable("Chennels");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.Subscribes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChennelId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChennelId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscribes");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categories")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ProfileImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Roles")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(5355)
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("VideoData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("VideoTitle")
                        .HasMaxLength(355)
                        .HasColumnType("nvarchar(355)");

                    b.Property<string>("VideoType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("WebApplication8.Models.Notify.Notify", b =>
                {
                    b.HasOne("WebApplication8.Models.Video.User", "UserList")
                        .WithMany("Notify")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserList");
                });

            modelBuilder.Entity("WebApplication8.Models.Quiz.DepOptionsList", b =>
                {
                    b.HasOne("WebApplication8.Models.Quiz.Quiz", "Quiz")
                        .WithMany("DepOptionsLists")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Quiz");
                });

            modelBuilder.Entity("WebApplication8.Models.Quiz.Quiz", b =>
                {
                    b.HasOne("WebApplication8.Models.Video.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.Chennel", b =>
                {
                    b.HasOne("WebApplication8.Models.Video.User", "User")
                        .WithMany("Chennels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.Subscribes", b =>
                {
                    b.HasOne("WebApplication8.Models.Video.Chennel", "Chennel")
                        .WithMany()
                        .HasForeignKey("ChennelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication8.Models.Video.User", "User")
                        .WithMany("Subscribers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chennel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.Video", b =>
                {
                    b.HasOne("WebApplication8.Models.Video.Chennel", "Channel")
                        .WithMany("Videos")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");
                });

            modelBuilder.Entity("WebApplication8.Models.Quiz.Quiz", b =>
                {
                    b.Navigation("DepOptionsLists");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.Chennel", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("WebApplication8.Models.Video.User", b =>
                {
                    b.Navigation("Chennels");

                    b.Navigation("Notify");

                    b.Navigation("Subscribers");
                });
#pragma warning restore 612, 618
        }
    }
}
