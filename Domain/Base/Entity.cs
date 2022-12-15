namespace Domain.Base
{
    public class Entity : Entity<int>
    {

    }

    public class Entity<Tkey> : EntityBase
    {
        public virtual Tkey Id { get; set; }
    }
}
