using BusinessObject;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public Member GetMemberByEmail(string email) => AuthDAO.GetMemberByEmail(email);

        public bool Login(string username, string password) => AuthDAO.Login(username, password);

        public bool Register(Member m) => AuthDAO.Register(m);
    }
}
