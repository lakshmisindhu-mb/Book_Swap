using System;
using System.Collections.Generic;
using Book_Swap_Models;
using Book_Swap_Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_Swap_DL;

public partial class BookSwapContext : DbContext
{
    public BookSwapContext()
    {
    }

    public BookSwapContext(DbContextOptions<BookSwapContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BookGenreList> BookGenreLists { get; set; }

    public virtual DbSet<BookList> BookLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserBookTransaction> UserBookTransactions { get; set; }

    public virtual DbSet<WishListBook> WishListBooks { get; set; }

    public virtual DbSet<UserRatings> UserRatings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Book_Swap;Integrated Security=True;TrustServerCertificate=true ;");
        => optionsBuilder.UseSqlServer("Data Source=mobacksquad.database.windows.net;Initial Catalog=Book_Swap;User id=mobacksquad;Password=dqbQ5223;");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookGenreList>(entity =>
        {
            entity.ToTable("Book_GenreList");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GenreName).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<BookList>(entity =>
        {
            entity.ToTable("Book_List");

            entity.Property(e => e.Author).HasMaxLength(150);
            entity.Property(e => e.BookName).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Edition).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Publisher).HasMaxLength(150);
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId).HasMaxLength(150);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UserKey).HasMaxLength(250);
            entity.Property(e => e.UserName).HasMaxLength(250);
            entity.Property(e => e.AverageRating).HasMaxLength(5);
        });

        modelBuilder.Entity<UserBookTransaction>(entity =>
        {
            entity.ToTable("User_Book_Transaction");

            entity.Property(e => e.Amount).HasMaxLength(50);
            entity.Property(e => e.BorrowDate)
                .HasColumnType("datetime")
                .HasColumnName("borrowDate");
            entity.Property(e => e.BorrowerId).HasColumnName("Borrower_Id");
            entity.Property(e => e.LenderId).HasColumnName("Lender_Id");
            entity.Property(e => e.ReturnDate)
                .HasColumnType("datetime")
                .HasColumnName("returnDate");
            entity.Property(e => e.Review).HasMaxLength(250);
        });

        modelBuilder.Entity<WishListBook>(entity =>
        {
            entity.ToTable("WishList_book");

            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.BookName).HasMaxLength(150);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DeadLineDate).HasColumnType("datetime");
            entity.Property(e => e.Edition).HasMaxLength(50);
            entity.Property(e => e.EmailId).HasMaxLength(150);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .HasColumnName("phone");
            entity.Property(e => e.Publisher).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(150);
            entity.Property(e => e.WishlistedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<UserRatings>(entity =>
        {
            entity.ToTable("User_Ratings");
            entity.Property(e => e.BorrowerId).HasColumnName("Borrower_Id");
            entity.Property(e => e.LenderId).HasColumnName("Lender_Id");
            entity.Property(e => e.Rating).HasMaxLength(5);
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
