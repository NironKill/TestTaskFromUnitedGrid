using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Chat.Domain.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}
