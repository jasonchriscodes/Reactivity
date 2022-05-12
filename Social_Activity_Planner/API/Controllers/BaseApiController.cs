using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // attribute
    [ApiController]
    [Route("api/[controller]")] // need to match url in the postman
    public class BaseApiController : ControllerBase
    {
    }
}
