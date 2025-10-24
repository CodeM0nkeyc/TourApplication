namespace TourApp.Persistence.EntityConfigs;

public class TourEntityConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.ToTable("Tours");
        
        builder.HasKey(x => x.Id);
        
        builder.HasOne(x => x.TourPricingDetails)
            .WithOne()
            .HasForeignKey<TourPricingDetails>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(x => x.State)
            .HasConversion<EnumToStringConverter<TourState>>()
            .HasColumnType("nvarchar(20)")
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();
        
        builder.Property(x => x.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()")
            .IsRequired();

        builder.Property(x => x.Difficulty)
            .HasConversion<EnumToStringConverter<TourDifficulty>>();

        builder.Property(x => x.Heading)
            .HasMaxLength(70);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Rating)
            .HasColumnType("real");

        builder.Property(x => x.DisplayImageName)
            .HasMaxLength(128);
        
        builder.HasCheckConstraint("CK_TourDetails_Rating", $"[{nameof(Tour.Rating)}] BETWEEN 0 AND 5");
        builder.HasCheckConstraint("CK_TourDetails_StartDate", $"[{nameof(Tour.StartDate)}] > CAST( GETUTCDATE() AS date)");
    }
}