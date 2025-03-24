using System;
using System.Collections.Generic;
using HotelBookingApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingApi.Data;

public partial class HotelBookingDbContext : DbContext
{
    public HotelBookingDbContext()
    {
    }

    public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    public virtual DbSet<HotelImage> HotelImages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomType> RoomTypes { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StatusType> StatusTypes { get; set; }

    public virtual DbSet<SupportTicket> SupportTickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC0754119643");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Room).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Rooms");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Status");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Bookings_Users");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Favorite__3214EC0731CB2FD7");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_Hotels");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Favorites_Users");
        });

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Hotels__3214EC07EEE002C6");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Rating)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Owner).WithMany(p => p.Hotels)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK_Hotels_Owner");
        });

        modelBuilder.Entity<Hotel>().HasQueryFilter(h => !h.IsDeleted);

        modelBuilder.Entity<HotelImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HotelIma__3214EC07F81CB620");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.IsBanner).HasDefaultValue(false);

            entity.HasOne(d => d.Hotel).WithMany(p => p.HotelImages)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK__HotelImag__Hotel__1CBC4616");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC0749162A58");

            entity.HasIndex(e => e.TransactionId, "UQ__Payments__55433A6A09E2D4DB").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TransactionId).HasMaxLength(255);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK_Payments_Bookings");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_PaymentMethods");

            entity.HasOne(d => d.Status).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Statuses");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Users");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PaymentM__3214EC07E6D902F0");

            entity.HasIndex(e => e.Name, "UQ__PaymentM__737584F695050D4E").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC07DFC18046");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK_Promotions_Hotels");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC076DFF623D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.HotelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Hotels");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Reviews_Users");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rooms__3214EC077DD03A3E");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.HotelId)
                .HasConstraintName("FK_Rooms_Hotels");

            entity.HasOne(d => d.RoomType).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.RoomTypeId)
                .HasConstraintName("FK_Rooms_RoomTypes");
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoomType__3214EC0733F7483A");

            entity.HasIndex(e => e.Name, "UQ__RoomType__737584F61E6FFCBE").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC077EAA8E50");

            entity.ToTable("Status");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(20);

            entity.HasOne(d => d.StatusType).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.StatusTypeId)
                .HasConstraintName("FK_Statuses_StatusTypes");
        });

        modelBuilder.Entity<StatusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StatusTy__3214EC07D2CA3AE9");

            entity.HasIndex(e => e.Name, "UQ__StatusTy__737584F654CE5AC0").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SupportT__3214EC07D965778D");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Subject).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Status).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupportTickets_Status");

            entity.HasOne(d => d.User).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SupportTickets_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0731E41216");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341E325A20").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("FK_Users_UserRole");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC079E02B73A");

            entity.ToTable("UserRole");

            entity.HasIndex(e => e.Name, "UQ__UserRole__737584F6F1DB49D4").IsUnique();

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
