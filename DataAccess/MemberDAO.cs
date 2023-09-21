using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        public static List<Member> GetMembers()
        {
            var list = new List<Member>();
            try
            {
                using (var context = new eStoreContext())
                {
                    list = context.Members.ToList();
                }
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }

        public static Member GetMember(int id)
        {
            Member member = new Member();
            try
            {
                using (var context = new eStoreContext())
                {
                    member = context.Members.FirstOrDefault(m => m.MemberId == id);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return member;
        }

        public static void CreateMember(Member member)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.Members.Add(member);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateMember(Member member)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    context.Entry<Member>(member).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteMember(Member member)
        {
            try
            {
                using (var context = new eStoreContext())
                {
                    var _member = context.Members.SingleOrDefault(c => c.MemberId == member.MemberId);
                    context.Members.Remove(_member);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
