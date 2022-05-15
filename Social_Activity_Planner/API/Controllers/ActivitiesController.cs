using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
 public class ActivitiesController : BaseApiController
 {

  // end points
  [HttpGet] // using get to get all activity (result will be shown in URL)
  public async Task<IActionResult> GetActivities()
  {
   return HandleResult(await Mediator.Send(new List.Query())); // get response back from mediator handler
  }

  [HttpGet("{id}")] // using get to get an activity (result will be shown in URL)
  public async Task<IActionResult> GetActivities(Guid id)
  {
   return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
  }

  [HttpPost] // using post method to create activity (safer than get)
  public async Task<IActionResult> CreateActivity(Activity activity)// IActionResul give access to http response task such as return OK, badrequest, not found
  {
   return HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));
  }

  [HttpPut("{id}")] // put for updating resources
  public async Task<IActionResult> EditActivity(Guid id, Activity activity) // add id to activity object before pass it to handler
  {
   activity.Id = id;
   return Ok(await Mediator.Send(new Edit.Command { Activity = activity }));
  }

  [HttpDelete("{id}")] // to delete resource
  public async Task<IActionResult> DeleteActivity(Guid id)// IActionResult (doest not require a type)
  {
   return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
  }
 }
}
