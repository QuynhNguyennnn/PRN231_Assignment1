using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interface
{
    public interface IMemberRepository
    {
        void InsertMember(Member m);
        Member GetMemberById(int id);
        void DeleteMember(Member m);
        void UpdateMember(Member m);
        List<Member> GetMembers();
    }
}
