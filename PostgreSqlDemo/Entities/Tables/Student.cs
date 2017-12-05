using PostgreSqlDemo.Bases;
using PostgreSqlDemo.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSqlDemo.Entities.Tables
{
    [Table("Student", Schema = "public")]
    public class Student : EntityVersionTenant
    {
        [Column("Name"), Required]
        public string Name { get; set; }

        [Column("StudentType"), Required]
        public StudentType StudentType { get; set; }
    }
}
