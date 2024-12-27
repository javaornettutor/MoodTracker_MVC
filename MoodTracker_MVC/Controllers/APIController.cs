using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoodTracker_MVC.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MoodTracker_MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class APIController : ControllerBase
    {
        private readonly MoodTracker2Context _context;

        public APIController(MoodTracker2Context context)
        {
            _context = context;
        }

        // GET: api/Moods
        [HttpGet("GetMoods")]        
        public async Task<ActionResult<IEnumerable<Mood>>> GetMoods()
        {
            return Ok(await _context.Moods.ToListAsync());
        }

        // GET: api/Moods/5
        [HttpGet("GetMood")]
        public async Task<IActionResult> GetMood(int id)
        {
            var mood = await _context.Moods.FindAsync(id);

            if (mood == null)
            {
                return NotFound();
            }
            string jsonString = JsonSerializer.Serialize(mood);
            return Ok(jsonString);
        }

        // POST: api/Moods
        [HttpPost("CreateMood")]
        public async Task<ActionResult<Mood>> CreateMood(Mood mood)
        {
            if (ModelState.IsValid)
            {
                _context.Moods.Add(mood);
                await _context.SaveChangesAsync();

                // Return CreatedAt with the route for the newly created resource
                return CreatedAtAction(nameof(GetMood), new { id = mood.MoodId }, mood);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Moods/5
        [HttpPut("UpdateMood")]
        public async Task<IActionResult> UpdateMood(int id, Mood mood)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(mood).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoodExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok();// NoContent(); // 204 No Content on successful update
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Moods/5
        [HttpDelete("DeleteMood")]
        public async Task<IActionResult> DeleteMood(int id)
        {
            var mood = await _context.Moods.FindAsync(id);
            if (mood == null)
            {
                return NotFound();
            }

            _context.Moods.Remove(mood);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content on successful deletion
        }

        private bool MoodExists(int id)
        {
            return _context.Moods.Any(e => e.MoodId == id);
        }
    }
}
