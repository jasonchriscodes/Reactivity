using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
 public class Create
 {
  public class Command : IRequest // command did not return anything
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
  public class Handler : IRequestHandler<Command>
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
   public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
   {
    this.context.Activities.Add(request.Activity); // adding the activity in memory, not database so does not need asynchronous version
    await this.context.SaveChangesAsync();
    return Unit.Value;
   }
  }
 }
}
