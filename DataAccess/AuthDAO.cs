using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuthDAO
    {
        public static bool Login(string email, string password)
        {
            Member member = new Member();
            try
            {
                using (var context = new eStoreContext())
                {
                    member = context.Members.FirstOrDefault(m => m.Email == email);
                    if (member != null)
                    {
                        if (member.Password.Equals(password))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Member GetMemberByEmail(string email)
        {
            Member member = new Member();
            try
            {
                using (var context = new eStoreContext())
                {
                    member = context.Members.FirstOrDefault(m => m.Email == email);
                    if (member != null)
                    {
                        return member;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    public static bool Register(Member member)
    {
        try
        {
            using (var context = new eStoreContext())
            {
                member = context.Members.FirstOrDefault(m => m.Email == member.Email);
                if (member != null)
                {
                    return false;
                }
                else
                {
                    context.Members.Add(member);
                    context.SaveChanges();
                    return true;
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
}
