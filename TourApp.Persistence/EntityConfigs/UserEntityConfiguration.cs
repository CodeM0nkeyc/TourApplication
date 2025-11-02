namespace TourApp.Persistence.EntityConfigs;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Identity)
            .WithOne(x => x.User)
            .HasForeignKey<UserIdentity>(x => x.Id)
            .HasPrincipalKey<User>(x => x.Id)
            .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .HasPrincipalKey(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(x => x.TourBookings)
            .WithOne(x => x.User)
            .HasForeignKey("UserId")
            .HasPrincipalKey(x => x.Id);

        builder.Property(x => x.FirstName)
            .HasMaxLength(70);

        builder.Property(x => x.MiddleName)
            .HasMaxLength(70);

        builder.Property(x => x.LastName)
            .HasMaxLength(70);
        
        builder.Property(x => x.Image)
            .HasColumnType("varbinary(max)");
        
        builder.OwnsOne(x => x.Address,
            address =>
            {
                address.Property(x => x.Country)
                    .HasMaxLength(50);
                
                address.Property(x => x.Region)
                    .HasMaxLength(100);
                
                address.Property(x => x.City)
                    .HasMaxLength(80);
            });
    }
}