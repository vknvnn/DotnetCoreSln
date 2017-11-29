using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MemoryDatabase;

namespace StepByStepReact.Backend.Todo
{
    [Route("api/Todo")]
    public class TodoController : Controller
    {
        private readonly MemoryContext _context;
        public TodoController(MemoryContext context)
        {
            _context = context;
            if (_context.Todos.Count() == 0)
            {
                _context.Todos.Add(new MemoryDatabase.Entities.Todo {
                    Name = "Do Something",
                    CreatedBy = "sys",
                    CreatedDate = DateTimeOffset.Now,
                    ModifiedBy = "sys",
                    ModifiedDate = DateTimeOffset.Now
                });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<MemoryDatabase.Entities.Todo> GetAll()
        {
            return _context.Todos.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = _context.Todos.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }
    }
}