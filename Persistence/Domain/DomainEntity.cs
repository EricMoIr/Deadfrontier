
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.Domain
{
    
    public class DomainEntity
    {
        [Key]
        public virtual int Id { get; }
    }
}
