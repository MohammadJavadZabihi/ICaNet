using System.ComponentModel.DataAnnotations;

namespace ICaNet.ApplicationCore.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual int Id { get; protected set; }
    }
}
