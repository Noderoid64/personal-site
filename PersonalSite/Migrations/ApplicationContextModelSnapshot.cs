﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonalSite.Infrastructure.EF;

#nullable disable

namespace PersonalSite.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.CommentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("integer");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PostId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.FileObjectEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("FileObjectType")
                        .HasColumnType("integer");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.Property<int>("PostAccessType")
                        .HasColumnType("integer");

                    b.Property<int>("ProfileId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.GoogleProfileEntity", b =>
                {
                    b.Property<string>("SourceId")
                        .HasColumnType("text");

                    b.Property<int>("ProfileEntityId")
                        .HasColumnType("integer");

                    b.HasKey("SourceId");

                    b.HasIndex("ProfileEntityId")
                        .IsUnique();

                    b.ToTable("GoogleProfiles");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.ProfileCredentialsEntity", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ProfileCredentials");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.ProfileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Nickname")
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RefreshTokenExpireOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.CommentEntity", b =>
                {
                    b.HasOne("PersonalSite.Core.Models.Entities.ProfileEntity", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonalSite.Core.Models.Entities.FileObjectEntity", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.FileObjectEntity", b =>
                {
                    b.HasOne("PersonalSite.Core.Models.Entities.FileObjectEntity", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("PersonalSite.Core.Models.Entities.ProfileEntity", "Profile")
                        .WithMany("Posts")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.GoogleProfileEntity", b =>
                {
                    b.HasOne("PersonalSite.Core.Models.Entities.ProfileEntity", "ProfileEntity")
                        .WithOne("GoogleProfileEntity")
                        .HasForeignKey("PersonalSite.Core.Models.Entities.GoogleProfileEntity", "ProfileEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileEntity");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.ProfileCredentialsEntity", b =>
                {
                    b.HasOne("PersonalSite.Core.Models.Entities.ProfileEntity", "Profile")
                        .WithOne("ProfileCredentials")
                        .HasForeignKey("PersonalSite.Core.Models.Entities.ProfileCredentialsEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.FileObjectEntity", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("PersonalSite.Core.Models.Entities.ProfileEntity", b =>
                {
                    b.Navigation("GoogleProfileEntity");

                    b.Navigation("Posts");

                    b.Navigation("ProfileCredentials")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
