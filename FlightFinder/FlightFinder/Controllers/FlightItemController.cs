using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightFinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightFinder.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class FlightItemController : ControllerBase
    {
       
            private readonly FlightContext _context;

            public FlightItemController(FlightContext context)
            {
                _context = context;
            }

            // GET: api/TodoItems
            [HttpGet]
            public async Task<ActionResult<IEnumerable<FlightItem>>> GetTodoItems()
            {
                return await _context.FlightItems.ToListAsync();
            }

            // GET: api/TodoItems/5
            [HttpGet("{id}")]
            public async Task<ActionResult<FlightItem>> GetTodoItem(long id)
            {
                var todoItem = await _context.FlightItems.FindAsync(id);

                if (todoItem == null)
                {
                    return NotFound();
                }

                return todoItem;
            }

            // POST: api/TodoItems
            // To protect from overposting attacks, please enable the specific properties you want to bind to, for
            // more details see https://aka.ms/RazorPagesCRUD.
            [HttpPost]
            public async Task<ActionResult<FlightItem>> PostTodoItem(FlightItem flightItem)
            {
                _context.FlightItems.Add(flightItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTodoItem), new { id = flightItem.Id }, flightItem);
            }


            private bool TodoItemExists(long id)
            {
                return _context.FlightItems.Any(e => e.Id == id);
            }
        }
    
}
