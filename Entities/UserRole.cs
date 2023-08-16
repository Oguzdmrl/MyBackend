using Entities.Base;

namespace Entities;

public class UserRole : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
    public virtual User User { get; set; }
}