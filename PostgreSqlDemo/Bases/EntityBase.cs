using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSqlDemo.Bases
{
    public class EntityBase
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
    }
}
