namespace TourApp.Persistence.EntityConfigs;

public class TourPricingDetailsEntityConfiguration : IEntityTypeConfiguration<TourPricingDetails>
{
    public void Configure(EntityTypeBuilder<TourPricingDetails> builder)
    {
        builder.ToTable("TourPricingDetails");
        
        builder.HasKey(x => x.Id);
    }
}