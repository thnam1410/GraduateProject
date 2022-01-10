namespace GraduateProject.Domain.Common;

public class Entity<TKey> : IEntity<TKey>
{
    public TKey Id { get; set; }
}