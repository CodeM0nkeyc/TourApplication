namespace TourApp.Persistence.EntityConfigs;

public class UserIdentityEntityConfiguration : IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        builder.ToTable("UserIdentities");
        
        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.ConfirmationCode, cfg =>
        {
            cfg.Property(cfg => cfg.ExpireAt)
                .HasDefaultValueSql("GETUTCDATE()");
        });

        builder.Property(x => x.Email)
            .HasMaxLength(255);

        builder.Property(x => x.DialCode)
            .HasMaxLength(8);
        
        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);
        
        builder.Property(x => x.PasswordHash)
            .HasMaxLength(512);

        builder.Property(x => x.PasswordSalt)
            .HasMaxLength(64);
    }
}