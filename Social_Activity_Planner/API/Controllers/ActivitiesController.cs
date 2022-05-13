using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly IMediator mediator;

        /// <summary>
        /// Inject mediator to ActivitiesController() so 
        /// that database is queried and return activity to the client
        /// </summary>
        public ActivitiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivities()
        {
            return await this.mediator.Send(new List.Query()); // get response back from mediator handler
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivities(Guid id)
        {
            return Ok();
        }
    }
}
