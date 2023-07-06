using Domain;
using MediatR;
using Persistence;

namespace Application.Members
{
    public class Details
    {
        public class Query : IRequest<Member>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Member>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;    
            }

            public async Task<Member> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Members.FindAsync(request.Id);
            }
        }
    }
}