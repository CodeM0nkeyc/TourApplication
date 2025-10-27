namespace TourApp.Persistence.EntityConfigs;

public class AppUserRoleEntityConfiguration : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.ToTable("AppUserRoles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .HasConversion<EnumToStringConverter<Role>>();
    }
}