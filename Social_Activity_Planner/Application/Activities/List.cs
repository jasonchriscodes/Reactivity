using System.Collections.Generic;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>>{} // to call use List.Query

        // handler class
        public class Handler : IRequestHandler<Query, List<Activity>>
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
            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await this.context.Activities.ToListAsync();
            }
        }
    }
}
