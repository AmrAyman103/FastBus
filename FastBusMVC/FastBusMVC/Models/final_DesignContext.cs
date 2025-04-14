using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FastBusMVC.Models
{
    public partial class final_DesignContext : DbContext
    {
        public final_DesignContext()
        {
        }

        public final_DesignContext(DbContextOptions<final_DesignContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Bus> Buses { get; set; } = null!;
        public virtual DbSet<SpBooking> SpBookings { get; set; } = null!;
        public virtual DbSet<TTicket> TTickets { get; set; } = null!;
        public virtual DbSet<Trip> Trips { get; set; } = null!;
        public virtual DbSet<TripBu> TripBus { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-R0QOES6Q\\SQLEXPRESS01;Database=final_Design;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.AdminType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("admin_type");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pass_word");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("c23");

                entity.ToTable("Booking");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.BookingDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("booking_date_time");

                entity.Property(e => e.Ticket)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ticket");

                entity.Property(e => e.TripId).HasColumnName("trip_id");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("c24");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("c39");
            });

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.ToTable("bus");

                entity.Property(e => e.BusId).HasColumnName("bus_id");

                entity.Property(e => e.BusTybe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bus_tybe");
            });

            modelBuilder.Entity<SpBooking>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("c103");

                entity.ToTable("sp_Booking");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.BookingDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("booking_date_time");

                entity.Property(e => e.Perpose)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("perpose");

                entity.Property(e => e.RequiredSeats)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("required_seats");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.SpBookings)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("c105");
            });

            modelBuilder.Entity<TTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("c21");

                entity.ToTable("t_ticket");

                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.AvilableTicket).HasColumnName("avilable_ticket");

                entity.Property(e => e.BookedTicket).HasColumnName("booked_ticket");

                entity.Property(e => e.NumberTicket).HasColumnName("number_ticket");

                entity.Property(e => e.TribId).HasColumnName("trib_id");

                entity.HasOne(d => d.Trib)
                    .WithMany(p => p.TTickets)
                    .HasForeignKey(d => d.TribId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("c22");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("trip");

                entity.Property(e => e.TripId).HasColumnName("trip_id");

                entity.Property(e => e.ArrivalCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("arrival_city");

                entity.Property(e => e.ArrivalDateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("arrival_date_time");

                entity.Property(e => e.BusId).HasColumnName("bus_id");

                entity.Property(e => e.DeparureCity)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Deparure_city");

                entity.Property(e => e.DeparureDataTime)
                    .HasColumnType("datetime")
                    .HasColumnName("Deparure_data_time");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.TripImage)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("trip_image");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.BusId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("c38");
            });

            modelBuilder.Entity<TripBu>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__TripBus__A25C5AA660E7BD98");

                entity.ToTable("TripBu");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.TripBus)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__TripBus__BusId__17036CC0");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.TripBus)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK__TripBus__TripId__17F790F9");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("c12");

                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "c14")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "c15")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "c16")
                    .IsUnique();

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.BathDate)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("bath_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gendr)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("gendr");

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(155)
                    .IsUnicode(false)
                    .HasColumnName("pass_word");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.UImage)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("u_image");

                entity.Property(e => e.Username)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
