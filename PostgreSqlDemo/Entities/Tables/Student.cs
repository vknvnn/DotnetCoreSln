using PostgreSqlDemo.Bases;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSqlDemo.Entities.Tables
{
    public enum StudentType : Int16
    {
        Normal = 1,
        Good = 2,
    }
    [Table("Student", Schema = "public")]
    public class Student : EntityVersionTenant
    {
        [Column("Name"), Required]
        public string Name { get; set; }

        [Column("StudentType"), Required]
        public StudentType StudentType { get; set; }
    }
}
