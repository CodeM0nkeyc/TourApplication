namespace TourApp.Persistence.EntityConfigs;

public class AppUserIdentityEntityConfiguration : IEntityTypeConfiguration<AppUserIdentity>
{
    public void Configure(EntityTypeBuilder<AppUserIdentity> builder)
    {
        builder.ToTable("AppUserIdentities");
        
        builder.HasKey(identity => identity.Id);

        builder.Property(identity => identity.Email)
            .HasMaxLength(255);
        
        builder.Property(identity => identity.PhoneNumber)
            .HasMaxLength(20);
        
        builder.Property(identity => identity.PasswordHash)
            .HasMaxLength(512);

        builder.Property(identity => identity.PasswordSalt)
            .HasMaxLength(64);
    }
}