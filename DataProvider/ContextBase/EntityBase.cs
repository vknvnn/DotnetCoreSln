using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextBase
{
    public class EntityBase
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
    }
}
