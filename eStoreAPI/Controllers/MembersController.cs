using DataAccess.Repository.Interface;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BusinessObject;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private IMemberRepository repository = new MemberRepository();

    [HttpGet]
    public ActionResult<List<Member>> GetMembers() => repository.GetMembers();

    [HttpGet("id")]
    public ActionResult<Member> GetMemebrById(int id)
    {
        return repository.GetMemberById(id);
    }

    [HttpPost]
    public IActionResult InserMember(Member member)
    {
        repository.InsertMember(member);
        return NoContent();
    }

    [HttpPost("edit/id")]
    public ActionResult UpdateMember(int id, Member member)
    {
        var mImp = repository.GetMemberById(id);
        if (mImp == null)
        {
            return NotFound();
        }
        repository.UpdateMember(member);
        return NoContent();
    }

    [HttpDelete("id")]
    public ActionResult DeleteMember(int id)
    {
        var m = repository.GetMemberById(id);
        if (m == null)
        {
            return NotFound();
        }
        repository.DeleteMember(m);
        return NoContent();
    }
}