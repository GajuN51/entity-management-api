using EntityAPI.Data;
using EntityAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EntityAPI.Controllers
{
    [Route("api/entity")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntityController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/entity/add
[HttpPost("add")]
public async Task<IActionResult> AddEntity([FromBody] Entity entity)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    try
    {
        _context.Entities.Add(entity);
        await _context.SaveChangesAsync();
        // Update this line to use SearchEntities method instead of GetEntityByNameOrPhone
        return CreatedAtAction(nameof(SearchEntities), new { name = entity.Name, phone = entity.PhoneNumber }, entity);
    }
    catch (DbUpdateException dbEx) when (dbEx.InnerException is Npgsql.PostgresException postgresEx &&
                                         postgresEx.SqlState == "23505") // Duplicate key violation
    {
        return Conflict(new { message = "A record with the same unique key already exists.", error = dbEx.Message });
    }
    catch (Exception ex)
    {
        // General error handler
        return StatusCode(500, new { message = "An unexpected error occurred while adding the record.", error = ex.Message });
    }
}


        // DELETE: api/entity/delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEntity(string name = null, string mobileNumber = null)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(mobileNumber))
                return BadRequest(new { message = "Please provide either a name or mobile number to delete." });

            try
            {
                var entity = await _context.Entities
                    .FirstOrDefaultAsync(e => e.Name == name || e.PhoneNumber == mobileNumber);

                if (entity == null)
                    return NotFound(new { message = "Record not found" });

                _context.Entities.Remove(entity);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Record deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while deleting the record.", error = ex.Message });
            }
        }

        // GET: api/entity/list
        [HttpGet("list")]
        public async Task<IActionResult> GetEntities()
        {
            try
            {
                var entities = await _context.Entities.ToListAsync();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while retrieving the records.", error = ex.Message });
            }
        }

        // GET: api/entity/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchEntities(string name = null, string mobileNumber = null)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(mobileNumber))
                return BadRequest(new { message = "Please provide either a name or mobile number to search." });

            try
            {
                var query = _context.Entities.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                    query = query.Where(e => e.Name.Contains(name));

                if (!string.IsNullOrEmpty(mobileNumber))
                    query = query.Where(e => e.PhoneNumber.Contains(mobileNumber));

                var results = await query.ToListAsync();

                if (results.Count == 0)
                    return NotFound(new { message = "No records found" });

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while searching for records.", error = ex.Message });
            }
        }

        // Helper method to validate mobile number format
        private bool IsValidMobileNumber(string mobileNumber)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"^\+?[0-9]{10,15}$");  // Adjust regex pattern as needed
            return regex.IsMatch(mobileNumber);
        }

    }  // <-- Missing closing brace for the class
}
