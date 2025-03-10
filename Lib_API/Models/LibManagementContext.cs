using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lib_API.Models;

public partial class LibManagementContext : DbContext
{
    public LibManagementContext()
    {
    }

    public LibManagementContext(DbContextOptions<LibManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookIssue> BookIssues { get; set; }

    public virtual DbSet<StudentProfile> StudentProfiles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Lib_Management;User Id=sa;Password=MyStrongPass123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Author__86516BCF100D53C7");

            entity.ToTable("Author");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("author_name");
            entity.Property(e => e.AuthorRating)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("author_rating");
            entity.Property(e => e.NumOfBooks).HasColumnName("num_of_books");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__490D1AE124718902");

            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.AddedBy).HasColumnName("added_by");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("author");
            entity.Property(e => e.AvailableQuantity).HasColumnName("available_quantity");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Isbn)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("isbn");
            entity.Property(e => e.Publication)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("publication");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShelfNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("shelf_number");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.AddedByNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.AddedBy)
                .HasConstraintName("FK__Books__added_by__3D5E1FD2");
        });

        modelBuilder.Entity<BookIssue>(entity =>
        {
            entity.HasKey(e => e.IssueId).HasName("PK__Book_Iss__D6185C393D6DBB5F");

            entity.ToTable("Book_Issue");

            entity.Property(e => e.IssueId).HasColumnName("issue_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.DueDate)
                .HasColumnType("datetime")
                .HasColumnName("due_date");
            entity.Property(e => e.IssuedBy).HasColumnName("issued_by");
            entity.Property(e => e.PenaltyAmount).HasColumnName("penalty_amount");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("datetime")
                .HasColumnName("return_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Book).WithMany(p => p.BookIssues)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__Book_Issu__book___403A8C7D");

            entity.HasOne(d => d.IssuedByNavigation).WithMany(p => p.BookIssues)
                .HasForeignKey(d => d.IssuedBy)
                .HasConstraintName("FK__Book_Issu__issue__4222D4EF");

            entity.HasOne(d => d.Student).WithMany(p => p.BookIssues)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Book_Issu__stude__412EB0B6");
        });

        modelBuilder.Entity<StudentProfile>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Student___2A33069A8E0F3C90");

            entity.ToTable("Student_Profiles");

            entity.HasIndex(e => e.RegistrationNumber, "UQ__Student___125DB2A3D65DB545").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Department)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.IsApproved).HasColumnName("is_approved");
            entity.Property(e => e.MaxBooksLimit).HasColumnName("max_books_limit");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("registration_number");
            entity.Property(e => e.Semester).HasColumnName("semester");
            entity.Property(e => e.StudentName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Student_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FF11BB0C0");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E6164850A1776").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
