namespace TourApp.Domain.Entities.User;

public class UserRole : EntityBase<int>
{
    public int Id { get; set; }
    public Role Name { get; set; }
    
    public ICollection<User> Users { get; set; }
}