using BusinessObject;
using DataAccess.DAO;
using DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void CreateMember(Member m) => MemberDAO.CreateMember(m);
        public Member GetMember(int id) => MemberDAO.GetMember(id);
        public void DeleteMember(Member m) => MemberDAO.DeleteMember(m);
        public void UpdateMember(Member m) => MemberDAO.UpdateMember(m);
        public List<Member> GetMembers() => MemberDAO.GetMembers();
    }
}
