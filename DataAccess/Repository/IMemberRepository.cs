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
        void CreateMember(Member m);
        Member GetMember(int id);
        void DeleteMember(Member m);
        void UpdateMember(Member m);
        List<Member> GetMembers();
    }
}
