using System.Collections.Generic;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
 public class List
 {
  public class Query : IRequest<Result<List<ActivityDto>>> { } // to call use List.Query

  // handler class
  public class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
  {
   private readonly DataContext context;
   private readonly IMapper mapper;

   public Handler(DataContext context, IMapper mapper)
   {
    this.mapper = mapper;
    this.context = context;
   }

   /// <summary>
   /// Task class that have access to query and cancellationToken
   /// </summary>
   /// <param name="request"></param>
   /// <param name="cancellationToken"></param>
   /// <returns>List of activities</returns>
   public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
   {
    var activities = await this.context.Activities
    .Include(a => a.Attendees)
    .ThenInclude(u => u.AppUser)
    .ToListAsync(cancellationToken);

    var activitiesToReturn = this.mapper.Map<List<ActivityDto>>(activities);

    return Result<List<ActivityDto>>.Success(activitiesToReturn);
   }
  }
 }
}
