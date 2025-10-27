using TourApp.Domain.Entities.Booking.Common;

namespace TourApp.Persistence.EntityConfigs;

public class TourBookingEntityConfiguration : IEntityTypeConfiguration<TourBooking>
{
    public void Configure(EntityTypeBuilder<TourBooking> builder)
    {
        builder.ToTable("TourBookings");
        
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.Tour)
            .WithOne()
            .HasForeignKey<TourBooking>("TourId")
            .HasPrincipalKey<Tour>(x => x.Id);

        builder.HasMany(x => x.TourCustomers)
            .WithMany(x => x.TourBookings);

        builder.Property(x => x.State)
            .HasConversion<EnumToStringConverter<BookingState>>()
            .HasColumnType("nvarchar(30)");

        builder.Property(x => x.BookingDate)
            .HasDefaultValueSql("GETUTCDATE()");

        builder.HasCheckConstraint("CK_TourBooking_TourCustomersCount", $"[{nameof(TourBooking.CustomersCount)}] > 0");
    }
}