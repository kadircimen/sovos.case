namespace Core.Persistence.Repositories;
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Updated { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
    public BaseEntity() { }
    public BaseEntity(int id) { Id = id; }
}
