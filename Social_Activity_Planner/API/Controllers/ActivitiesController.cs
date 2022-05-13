﻿using Application.Activities;
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
            return Ok();
        }
    }
}
