using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await Mediator.Send(new List.Query()); // get response back from mediator handler
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivities(Guid id)
        {
            return await Mediator.Send(new Details.Query {Id = id});
        }
        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)// IActionResul give access to http response task such as return OK, badrequest, not found
        {
            return Ok(await Mediator.Send(new Create.Command { Activity = activity }));
        }
    }
}
