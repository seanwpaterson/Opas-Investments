using MediatR;
using Persistence;

namespace Application.Members
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;   
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var member = await _context.Members.FindAsync(request.Id);

                _context.Remove(member);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}