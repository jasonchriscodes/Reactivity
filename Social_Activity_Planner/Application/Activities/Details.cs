using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
 public class Details
 {
  /// <summary>
  /// Query class that fetching activity 
  /// </summary> return activity
  public class Query : IRequest<Result<Activity>>
  {
   public Guid Id { get; set; }
  }
  // 
  /// <summary>
  /// Handler class
  /// </summary> return activity
  public class Handler : IRequestHandler<Query, Result<Activity>>
  {
   private readonly DataContext context;

   /// <summary>
   // Handler constructor
   /// </summary>
   /// <param name="context"></param>
   public Handler(DataContext context)
   {
    this.context = context;
   }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="request"></param>
   /// <param name="cancellationToken"></param>
   /// <returns>activity of specific id</returns>
   public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
   {
    var activity = await this.context.Activities.FindAsync(request.Id);
    return Result<Activity>.Success(activity);
   }
  }
 }
}
