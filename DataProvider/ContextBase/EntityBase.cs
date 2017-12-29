using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace ContextBase
{
    public class EntityBase
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }

        /// <summary>
        /// When set this property is true, that mean: the entity consitant with DB by ORM
        /// </summary>
        [NotMapped]        
        public bool IsNoneTracking { get; set; }

        /// <summary>
        /// Set config to tracking change of entity
        /// </summary>
        [NotMapped]
        public TrackingConfig TrackingConfig { get; set; }
    }
}
