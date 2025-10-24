namespace TourApp.Domain.Entities.User;

public class AppUserRole : EntityBase<int>
{
    public int Id { get; set; }
    public Role Name { get; set; }
    
    public ICollection<AppUser> Users { get; set; }
}