using PostgreSqlDemo.Context;
using PostgreSqlDemo.Entities.Enums;
using PostgreSqlDemo.Entities.Tables;
using System;

namespace PostgreSqlDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new PortgreSqlDemoContext())
            {
                db.Students.Add(new Student
                {
                    CreatedDate = DateTimeOffset.Now,
                    ModifiedDate = DateTimeOffset.Now,
                    CreatedBy = "nghiepvo",
                    ModifiedBy = "nghiepvo",
                    Version = 0,
                    TenantId = Guid.NewGuid(),
                    Name = "Võ Kế Nghiệp",
                    StudentType = StudentType.Normal
                });
                db.SaveChanges();
            }
        }
    }
}
