using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {


        private readonly ILogger<UserController> _logger;
        private readonly MyDbContext _dbContext;


        public UserController(ILogger<UserController> logger, MyDbContext myDbContext)
        {
            _logger = logger;
            _dbContext = myDbContext;

        }


        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);

                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest("User ID in URL does not match the body.");
            }

            var existingUser = await _dbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }

            _dbContext.Entry(existingUser).CurrentValues.SetValues(user);

            try
            {
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {

            var existingUser = await _dbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found.");
            }
            _dbContext.Users.Remove(existingUser);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
