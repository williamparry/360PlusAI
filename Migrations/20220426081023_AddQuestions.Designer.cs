﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ThreeSixtyPlusAI.Data;

#nullable disable

namespace ThreeSixtyPlusAI.Migrations
{
    [DbContext(typeof(ThreeSixtyPlusAIContext))]
    [Migration("20220426081023_AddQuestions")]
    partial class AddQuestions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("main")
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FriendlyName")
                        .HasColumnType("text");

                    b.Property<string>("Xml")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys", "main");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("QuestionCategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("QuestionCategoryId");

                    b.ToTable("Questions", "main");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3abbbbe4-9ec6-40f5-9332-d9c6b2b471ad"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Is this manager effective at solving problems?"
                        },
                        new
                        {
                            Id = new Guid("6ec59213-067f-4bef-9a5a-f9258823cb25"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Does this manager treat others respectfully?"
                        },
                        new
                        {
                            Id = new Guid("95ad7c70-84c1-4edd-bd6e-190769e02b08"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Do the actions of this manager inspire growth and development in others?"
                        },
                        new
                        {
                            Id = new Guid("f2acb881-da85-4b25-8081-936837a907fd"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Is this manager able to resolve conflict appropriately?"
                        },
                        new
                        {
                            Id = new Guid("ac2ec8f3-9042-4fad-b918-5ebde5e7c765"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Do you receive constructive and helpful feedback from this manager?"
                        },
                        new
                        {
                            Id = new Guid("0c74485e-ae23-4db9-af0b-1eafa4821e2b"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Is this manager available to provide help and feedback when you want it?"
                        },
                        new
                        {
                            Id = new Guid("27995e3e-c61a-400e-b5e6-02d7805d18e8"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "When making important decisions, does this manager consider the opinions of others?"
                        },
                        new
                        {
                            Id = new Guid("aca2ecf3-1b76-4631-a49b-f2f823b1d8d9"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Does this manager consistently reward employees for good performance or behavior?"
                        },
                        new
                        {
                            Id = new Guid("8074d239-5c43-4af6-960b-46ebd5d30cf1"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Do you feel this manager sets clear direction that aligns with the organization's strategy?"
                        },
                        new
                        {
                            Id = new Guid("21a16f98-155c-4e2b-acb9-09e887a001d6"),
                            QuestionCategoryId = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionText = "Does this manager always control emotions and behavior, even when faced with high-conflict or stressful situations?"
                        },
                        new
                        {
                            Id = new Guid("9cb16d51-bc92-49a3-91cb-d4c966089c03"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "What does this employee do well?"
                        },
                        new
                        {
                            Id = new Guid("5f57df7d-7210-445e-a2bc-60608f189b91"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "What does this employee need to improve on?"
                        },
                        new
                        {
                            Id = new Guid("08d16d66-080a-4a3d-8b7f-e84f1246e01d"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Does this employee act professionally?"
                        },
                        new
                        {
                            Id = new Guid("069f8124-ae0f-49b5-800e-cc687c9067fb"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Does this employee use their time effectively?"
                        },
                        new
                        {
                            Id = new Guid("3eb7d9f8-e9f4-469c-b292-1d6e53e633e0"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Do you believe this employee is honest, ethical and trustworthy?"
                        },
                        new
                        {
                            Id = new Guid("24a957f0-261d-491b-b8b7-0929d0dd5adb"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Has this employee shown they apply feedback they receive to learn and grow?"
                        },
                        new
                        {
                            Id = new Guid("938efc50-57f9-4166-b26c-c604f67ae344"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Does this employee prioritize the needs of the customer?"
                        },
                        new
                        {
                            Id = new Guid("8c445394-cbf1-43c1-b33b-1e504f858fae"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Does this employee show initiative to solve problems?"
                        },
                        new
                        {
                            Id = new Guid("10e70c40-a226-42d6-8387-a66c2a021920"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Does this employee motivate others to reach goals?"
                        },
                        new
                        {
                            Id = new Guid("e8b77d88-6720-48dd-8007-48221b777cf1"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Has this employee shown initiative to take the lead on team projects or assignments?"
                        },
                        new
                        {
                            Id = new Guid("65f77b25-b49b-4d8e-b8c1-00f02abc23b5"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Do you believe this employee knows and represents our company's goals and values?"
                        },
                        new
                        {
                            Id = new Guid("c2156adc-d47b-4d30-aff2-84c8fbebe963"),
                            QuestionCategoryId = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionText = "Can you rely on this employee to follow through with his/her promises and responsibilities?"
                        });
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.QuestionCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("QuestionCategoryIcon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("QuestionCategoryTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("QuestionCategories", "main");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f2cfc2d8-0827-4c95-b229-b3a312c54d56"),
                            QuestionCategoryIcon = "bi-person-circle",
                            QuestionCategoryTitle = "Manager"
                        },
                        new
                        {
                            Id = new Guid("b19968c8-31ef-44d1-91c1-83463d7976bf"),
                            QuestionCategoryIcon = "bi-person-badge",
                            QuestionCategoryTitle = "Employee"
                        });
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReview", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ThreeSixtyReviews", "main");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReviewAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ThreeSixtyReviewQuestionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ThreeSixtyReviewerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ThreeSixtyReviewerId");

                    b.ToTable("ThreeSixtyReviewAnswer", "main");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReviewer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("HasFinished")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ThreeSixtyReviewId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ThreeSixtyReviewId");

                    b.ToTable("ThreeSixtyReviewer", "main");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReviewQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ThreeSixtyReviewId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ThreeSixtyReviewId");

                    b.ToTable("ThreeSixtyReviewQuestion", "main");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.Question", b =>
                {
                    b.HasOne("ThreeSixtyPlusAI.Models.QuestionCategory", "QuestionCategory")
                        .WithMany()
                        .HasForeignKey("QuestionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionCategory");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReviewAnswer", b =>
                {
                    b.HasOne("ThreeSixtyPlusAI.Models.ThreeSixtyReviewer", "ThreeSixtyReviewer")
                        .WithMany("ThreeSixtyReviewAnswers")
                        .HasForeignKey("ThreeSixtyReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThreeSixtyReviewer");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReviewer", b =>
                {
                    b.HasOne("ThreeSixtyPlusAI.Models.ThreeSixtyReview", "ThreeSixtyReview")
                        .WithMany("ThreeSixtyReviewers")
                        .HasForeignKey("ThreeSixtyReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThreeSixtyReview");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReviewQuestion", b =>
                {
                    b.HasOne("ThreeSixtyPlusAI.Models.ThreeSixtyReview", "ThreeSixtyReview")
                        .WithMany("ThreeSixtyReviewQuestions")
                        .HasForeignKey("ThreeSixtyReviewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThreeSixtyReview");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReview", b =>
                {
                    b.Navigation("ThreeSixtyReviewQuestions");

                    b.Navigation("ThreeSixtyReviewers");
                });

            modelBuilder.Entity("ThreeSixtyPlusAI.Models.ThreeSixtyReviewer", b =>
                {
                    b.Navigation("ThreeSixtyReviewAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
