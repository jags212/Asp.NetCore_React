using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Domain.Activity>
        {
            public Guid Id {get;set;}
        }

        public class Handler : IRequestHandler<Query, Domain.Activity>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Domain.Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var activity = await _context.Activities.FindAsync(request.Id);
                 if (activity == null) {
                    throw new RestException(HttpStatusCode.NotFound, new 
                    {activity = "Not found"});
                }

                return activity;
            }
        }
    }
}