using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Controllers
{
    public class MembersController : BaseController
    {
        private readonly DataContext _context;

        public MembersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(Guid id)
        {
            return await _context.Members.FindAsync(id);
        }
    }
}