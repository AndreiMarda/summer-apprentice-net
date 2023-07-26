using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TicketManagementApplicationAPI.Model;

public partial class TicketManagementApplicationContext : DbContext
{
    public TicketManagementApplicationContext()
    {
    }

    public TicketManagementApplicationContext(DbContextOptions<TicketManagementApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\MSSQLSERVER06;Initial Catalog=TicketManagementApplication;Integrated Security=True;TrustServerCertificate=True;encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB852FB2A4AA");

            entity.ToTable("customer");

            entity.HasIndex(e => e.Email, "UQ__customer__AB6E61647B893E1E").IsUnique();

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasColumnType("text")
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__event__2370F7274597227E");

            entity.ToTable("event");

            entity.Property(e => e.EventId)
                .ValueGeneratedNever()
                .HasColumnName("event_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.EventDescription)
                .HasColumnType("text")
                .HasColumnName("event_description");
            entity.Property(e => e.EventName)
                .HasColumnType("text")
                .HasColumnName("event_name");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.VenueId).HasColumnName("venue_id");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_type_id__event_type");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__event__venue_id__29572725");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__event_ty__BB84C6F31F47C6EC");

            entity.ToTable("event_type");

            entity.HasIndex(e => e.Name, "UQ__event_ty__72E12F1BB8A0CD23").IsUnique();

            entity.Property(e => e.EventTypeId)
                .ValueGeneratedNever()
                .HasColumnName("event_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__465962290D2B2368");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.NumberOfTickets).HasColumnName("number_of_tickets");
            entity.Property(e => e.OrderedAt)
                .HasColumnType("date")
                .HasColumnName("ordered_at");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_OrderDetails_user_ID");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ticket_category_id__ticket_category");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__ticket_c__3FC8DEA28C5A0A14");

            entity.ToTable("ticket_category");

            entity.Property(e => e.TicketCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("ticket_category_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_event_id__event");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__venue__82A8BE8DBFA3F8CD");

            entity.ToTable("venue");

            entity.Property(e => e.VenueId)
                .ValueGeneratedNever()
                .HasColumnName("venue_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.Location)
                .HasColumnType("text")
                .HasColumnName("location");
            entity.Property(e => e.Type)
                .HasColumnType("text")
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
