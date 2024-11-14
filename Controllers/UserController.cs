using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skeleton.Database;
using Skeleton.Requests;
using Skeleton.Services;

namespace Skeleton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService _service;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> SingUp([FromBody] SingUpRequest request)
        {
            Result<User> result = await _service.SingUp(request);
            return result.Match<IActionResult>(
                m => CreatedAtAction(nameof(Get), new { idOrSlug = m.Id }),
                err => BadRequest());
        }
        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return null;
        }
    }
}
