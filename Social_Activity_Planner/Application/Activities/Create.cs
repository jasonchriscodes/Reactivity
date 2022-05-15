using Application.Core;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
 public class Create
 {
  public class Command : IRequest<Result<Unit>> // command did not return anything
  {
   public Activity Activity { get; set; }
  }

  public class CommandValidator : AbstractValidator<Command>
  {
   public CommandValidator()
   {
    RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
   }
  }
  public class Handler : IRequestHandler<Command, Result<Unit>>
  {
   private readonly DataContext context;

   public Handler(DataContext context) // inject data context
   {
    this.context = context;
   }
   /// <summary>
   /// 
   /// </summary>
   /// <param name="request"></param>
   /// <param name="cancellationToken"></param>
   /// <returns>value of unit, letting API know we finish this method</returns>
   public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
   {
    this.context.Activities.Add(request.Activity); // adding the activity in memory, not database so does not need asynchronous version
    var result = await this.context.SaveChangesAsync() > 0; // if save changes is successful, result will be true

    if (!result) return Result<Unit>.Failure("Failed to create activity");

    return Result<Unit>.Success(Unit.Value);
   }
  }
 }
}
