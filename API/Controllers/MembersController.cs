using Application.Members;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MembersController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Member>>> GetMembers()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult> CreateMember(Member member)
        {
            return Ok(await Mediator.Send(new Create.Command{Member = member}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMember(Guid id, Member member)
        {
            member.Id = id;

            return Ok(await Mediator.Send(new Update.Command{Member = member}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command{Id = id}));
        }
    }
}