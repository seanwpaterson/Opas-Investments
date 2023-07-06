using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Members
{
    public class List
    {
        public class Query : IRequest<List<Member>> {}

        public class Handler : IRequestHandler<Query, List<Member>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;    
            }

            public Task<List<Member>> Handle(Query request, CancellationToken cancellationToken)
            {
                return _context.Members.ToListAsync();
            }
        }
    }
}