using System.Linq;
using DataLayer.Context;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class LoginRepository : ILoginRepository
    {
        private readonly CmsContext _context;

        public LoginRepository(CmsContext context)
        {
            _context = context;
        }

        public bool UserExists(string username, string password)
        {
            return _context.AdminLogins.Any(u => u.Username == username && u.Password == password);
        }
    }
}
