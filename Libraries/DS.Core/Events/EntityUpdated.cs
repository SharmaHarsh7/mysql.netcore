
using DS.Core.Domain;
using DS.Domain;

namespace DS.Core.Events
{
    public class EntityUpdated<T> where T :BaseEntity
    {
        public EntityUpdated(T entity)
        {
            this.Entity = entity;
        }

        public T Entity { get; private set; }
    }
}
