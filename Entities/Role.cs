using Entities.Base;

namespace Entities
{
    public class Role : Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}