using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkmWebApi.Model;

namespace TodoApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase {
        private readonly TodoContext todoContext;

        public TodoController (TodoContext context) {
            todoContext = context;

            if (todoContext.TodoItems.Count () == 0) {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                todoContext.TodoItems.Add (new TodoItem { Name = "Item1" });
                todoContext.SaveChanges ();
            }
        }

        // GET: api/Todo
        [HttpGet]
        [EnableCors ()]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems () {
            return await todoContext.TodoItems.ToListAsync ();
        }

        // GET: api/Todo/5
        [HttpGet ("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem (long id) {
            var todoItem = await todoContext.TodoItems.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            return todoItem;
        }

        // POST: api/Todo
        [HttpPost]
        [EnableCors ("AnotherPolicy")]
        public async Task<ActionResult<TodoItem>> PostTodoItem (TodoItem item) {
            todoContext.TodoItems.Add (item);
            await todoContext.SaveChangesAsync ();

            return CreatedAtAction (nameof (GetTodoItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut ("{id}")]
        [EnableCors ("AnotherPolicy")]
        public async Task<IActionResult> PutTodoItem (long id, TodoItem item) {
            if (id != item.Id) {
                return BadRequest ();
            }

            todoContext.Entry (item).State = EntityState.Modified;
            await todoContext.SaveChangesAsync ();

            return NoContent ();

        }

        // DELETE: api/Todo/5
        [HttpDelete ("{id}")]
        [EnableCors ("AnotherPolicy")]
        public async Task<IActionResult> DeleteTodoItem (long id) {
            var todoItem = await todoContext.TodoItems.FindAsync (id);

            if (todoItem == null) {
                return NotFound ();
            }

            todoContext.TodoItems.Remove (todoItem);
            await todoContext.SaveChangesAsync ();

            return NoContent ();
        }
    }
}