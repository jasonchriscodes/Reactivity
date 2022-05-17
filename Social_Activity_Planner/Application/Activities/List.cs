using System.Collections.Generic;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
 public class List
 {
  public class Query : IRequest<Result<List<Activity>>> { } // to call use List.Query

  // handler class
  public class Handler : IRequestHandler<Query, Result<List<Activity>>>
  {
   private readonly DataContext context;

   public Handler(DataContext context)
   {
    this.context = context;
   }

   /// <summary>
   /// Task class that have access to query and cancellationToken
   /// </summary>
   /// <param name="request"></param>
   /// <param name="cancellationToken"></param>
   /// <returns>List of activities</returns>
   public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
   {
    var activities = await this.context.Activities
    .Include(a => a.Attendees)
    .ThenInclude(u => u.AppUser)
    .ToListAsync(cancellationToken);

    return Result<List<Activity>>.Success(activities);
   }
  }
 }
}
