using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserCRUD.Models;
using UserCRUD.Services;

namespace UserCRUD.Controllers
{
    /// <summary>
    /// UserController Class with all the APIs to GET, CREATE, UPDATE and DELETE documents from MongoDB Collection.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _UserService;

        public UserController(UserService UserService)
        {
            _UserService = UserService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() =>
            
            _UserService.Get();

        [HttpGet("{id:length(24)}", Name = "GetUserList")]
        public ActionResult<User> Get(string id)
        {
            var usr = _UserService.Get(id);
            if (usr == null)
            {
                return NotFound();
            }
            return usr;
        }
        [HttpPost(Name = "GetSpecificUser")]
        public ActionResult<User> Post([FromBody] User u)
        {
            User newUser = _UserService.Create(u);
            return newUser;
        }
        [HttpPut("{id:length(24)}",Name ="UpdateSpecificUser")]
        public ActionResult Put(string id, [FromBody] User u)
        {
            var usr = _UserService.Get(id);
            if (usr == null)
            {
                return NotFound();
            }
            _UserService.Put(id, u);
            return new OkResult();
        }

        [HttpDelete("{id:length(24)}",Name ="DeleteUser")]
        public IActionResult Delete(string id)
        {
            var usr = _UserService.Get(id);
            if (usr == null)
            {
                return NotFound();
            }
            bool isDeleted = _UserService.Remove(id);
            return new OkResult();
        }
    }
}
