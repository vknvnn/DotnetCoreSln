using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryDatabase.Entities
{
    public class Todo: EntityBase
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
