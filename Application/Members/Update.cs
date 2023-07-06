using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Members
{
    public class Update
    {
        public class Command : IRequest
        {
            public Member Member { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;   
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var member = await _context.Members.FindAsync(request.Member.Id);

                _mapper.Map(request.Member, member);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}