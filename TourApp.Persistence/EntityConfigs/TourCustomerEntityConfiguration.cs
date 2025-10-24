namespace TourApp.Persistence.EntityConfigs;

public class TourCustomerEntityConfiguration : IEntityTypeConfiguration<TourCustomer>
{
    public void Configure(EntityTypeBuilder<TourCustomer> builder)
    {
        builder.ToTable("TourCustomers");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(70);
        
        builder.Property(x => x.MiddleName)
            .HasMaxLength(70);
        
        builder.Property(x => x.LastName)
            .HasMaxLength(70);

        builder.HasCheckConstraint("CK_TourCustomer_Age", $"[{nameof(TourCustomer.Age)}] > 0");
    }
}