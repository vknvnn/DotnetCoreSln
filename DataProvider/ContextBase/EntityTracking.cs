﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContextBase
{
    public class EntityTracking : EntityVersionTenant
    {
        [Column("CreatedDate")]
        public DateTimeOffset CreatedDate { get; set; }
        [Column("ModifiedDate")]
        public DateTimeOffset ModifiedDate { get; set; }
        [Column("CreatedBy"), Required]
        public string CreatedBy { get; set; }
        [Column("ModifiedBy"), Required]
        public string ModifiedBy { get; set; }        
    }
}
