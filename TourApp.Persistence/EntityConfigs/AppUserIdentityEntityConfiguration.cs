namespace TourApp.Persistence.EntityConfigs;

public class AppUserIdentityEntityConfiguration : IEntityTypeConfiguration<AppUserIdentity>
{
    public void Configure(EntityTypeBuilder<AppUserIdentity> builder)
    {
        builder.ToTable("AppUserIdentities");
        
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.ConfirmationCode, cfg =>
        {
            cfg.Property(cfg => cfg.ExpireAt)
                .HasDefaultValueSql("GETUTCDATE()");
        });

        builder.Property(x => x.Email)
            .HasMaxLength(255);
        
        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);
        
        builder.Property(x => x.PasswordHash)
            .HasMaxLength(512);

        builder.Property(x => x.PasswordSalt)
            .HasMaxLength(64);
    }
}