namespace Nogupe.Web.Entities
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
        object IBaseEntity.Id
        {
            get => Id;
            set => Id = (T)value;
        }
    }
}
