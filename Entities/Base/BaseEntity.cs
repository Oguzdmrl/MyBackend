namespace Entities.Base;

public class Entity<TKey>
{
    public TKey Id { get; set; }
    public bool WithDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}