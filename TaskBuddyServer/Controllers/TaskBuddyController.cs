using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBuddyServer.Data;
using TaskBuddyClassLibrary.Models;

namespace TaskBuddyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskBuddyController : ControllerBase
    {
        private readonly TaskBuddyDbContext _context;

        public TaskBuddyController(TaskBuddyDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskBuddy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskBuddyTask>>> GetTaskBuddyTasks()
        {
            return await _context.TaskBuddyTasks.ToListAsync();
        }

        // GET: api/TaskBuddy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskBuddyTask>> GetTaskBuddyTask(int id)
        {
            var task = await _context.TaskBuddyTasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/TaskBuddy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskBuddyTask(int id, TaskBuddyTask task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskBuddyTaskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskBuddy
        [HttpPost]
        public async Task<ActionResult<TaskBuddyTask>> PostTaskBuddyTask(TaskBuddyTask task)
        {
            _context.TaskBuddyTasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskBuddyTask), new { id = task.Id }, task);
        }

        // DELETE: api/TaskBuddy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskBuddyTask(int id)
        {
            var task = await _context.TaskBuddyTasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.TaskBuddyTasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskBuddyTaskExists(int id)
        {
            return _context.TaskBuddyTasks.Any(e => e.Id == id);
        }
    }
}
