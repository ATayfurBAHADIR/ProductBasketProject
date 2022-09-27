using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        //[AllowAnonymous]
        public async Task<ActionResult<bool>> Login(string userName, string password)
        {

            var user = _userRepository.UserControl(userName, password);
            if (user)
                return Ok(user);
            else
                return Unauthorized();
        }

        [HttpPost]
        [Route("Add")]
        [AllowAnonymous]
        public async Task<ActionResult<bool>> Add(string userName, string password)
        {
            var oldUser = _userRepository.Filter(x => x.Username == userName).Count() > 0;
            if (oldUser)
            {
                return BadRequest("Daha önce böyle bir kullanıcı eklenmiştir.");
            }

            var user = _userRepository.AddUser(userName, password);
            if (user)
                return Ok(user);
            else
                return BadRequest("Hata oluştu.");

        }
    }
}
