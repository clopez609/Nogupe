namespace Nogupe.Web.Entities
{
    public interface IEntity<T> : IBaseEntity
    {
        new T Id { get; set; }
    }
}
