using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using AutoMapper;
using DataAccess.Repository;
using DataAccess.Repository.Interface;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private IMemberRepository repository = new MemberRepository();
        private readonly IMapper _mapper;

        public MembersController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public ActionResult<IEnumerable<Member>> GetCategories()
        {
            return repository.GetMembers();
        }
    }
}
