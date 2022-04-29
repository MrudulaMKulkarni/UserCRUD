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
        public ActionResult<List<User>> Get()
        {
            var usrs =_UserService.Get();
            return Ok(usrs);
        }
        [HttpGet("{id:length(24)}", Name = "GetUserList")]
        public ActionResult<User> Get(string id)
        {
            var usr = _UserService.Get(id);
            if (usr == null)
            {
                return NotFound();
            }
            return Ok(usr);
        }
        [HttpPost(Name = "GetSpecificUser")]
        public ActionResult Post([FromBody] User u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newUser = _UserService.Create(u);
            return CreatedAtAction("Get", new { id = newUser._id }, newUser);
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
            return Ok();
        }

        [HttpDelete("{id:length(24)}",Name ="DeleteUser")]
        public IActionResult Delete(string id)
        {
            var usr = _UserService.Get(id);
            if (usr == null)
            {
                return NotFound();
            }
            _UserService.Remove(id);
            return Ok();
        }
    }
}
