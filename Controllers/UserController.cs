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


        [HttpGet(Name = "GetUser")]
        public  ActionResult<User> Get(int id)
        {

            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
            

        }
    }
}
