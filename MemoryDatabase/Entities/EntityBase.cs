using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryDatabase.Entities
{
    public class EntityBase
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
