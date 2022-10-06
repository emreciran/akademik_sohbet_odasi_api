﻿// <auto-generated />
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220913145054_updated-mig13092022")]
    partial class updatedmig13092022
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EntityLayer.Concrete.Answer", b =>
                {
                    b.Property<int>("Answer_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AnswerContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Question_ID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("Answer_ID");

                    b.HasIndex("Question_ID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Comment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Question_ID")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("Question_ID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Question", b =>
                {
                    b.Property<int>("Question_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("View")
                        .HasColumnType("int");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("Question_ID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("EntityLayer.Concrete.QuestionTag", b =>
                {
                    b.Property<int>("Question_ID")
                        .HasColumnType("int");

                    b.Property<int>("Tag_ID")
                        .HasColumnType("int");

                    b.HasKey("Question_ID", "Tag_ID");

                    b.HasIndex("Tag_ID");

                    b.ToTable("QuestionTags");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Tag", b =>
                {
                    b.Property<int>("Tag_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Tag_ID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Answer", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("Question_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Comment", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Question", "Question")
                        .WithMany("Comments")
                        .HasForeignKey("Question_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("EntityLayer.Concrete.QuestionTag", b =>
                {
                    b.HasOne("EntityLayer.Concrete.Question", "Question")
                        .WithMany("QuestionTags")
                        .HasForeignKey("Question_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityLayer.Concrete.Tag", "Tag")
                        .WithMany("QuestionTags")
                        .HasForeignKey("Tag_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Question", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Comments");

                    b.Navigation("QuestionTags");
                });

            modelBuilder.Entity("EntityLayer.Concrete.Tag", b =>
                {
                    b.Navigation("QuestionTags");
                });
#pragma warning restore 612, 618
        }
    }
}