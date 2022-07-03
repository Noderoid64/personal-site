﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PersonalSite.Infrastructure.EF;

#nullable disable

namespace PersonalSite.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220703100336_updateProfileCredentials5")]
    partial class updateProfileCredentials5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PersonalSite.Core.Entities.GoogleProfileEntity", b =>
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

            modelBuilder.Entity("PersonalSite.Core.Entities.ProfileCredentialsEntity", b =>
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

            modelBuilder.Entity("PersonalSite.Core.Entities.ProfileEntity", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("PersonalSite.Core.Entities.GoogleProfileEntity", b =>
                {
                    b.HasOne("PersonalSite.Core.Entities.ProfileEntity", "ProfileEntity")
                        .WithOne("GoogleProfileEntity")
                        .HasForeignKey("PersonalSite.Core.Entities.GoogleProfileEntity", "ProfileEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProfileEntity");
                });

            modelBuilder.Entity("PersonalSite.Core.Entities.ProfileCredentialsEntity", b =>
                {
                    b.HasOne("PersonalSite.Core.Entities.ProfileEntity", "Profile")
                        .WithOne("ProfileCredentials")
                        .HasForeignKey("PersonalSite.Core.Entities.ProfileCredentialsEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PersonalSite.Core.Entities.ProfileEntity", b =>
                {
                    b.Navigation("GoogleProfileEntity");

                    b.Navigation("ProfileCredentials")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
