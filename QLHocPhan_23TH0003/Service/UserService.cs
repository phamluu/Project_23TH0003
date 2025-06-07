using QLHocPhan_23TH0003.Data;

namespace QLHocPhan_23TH0003.Service
{
    public class UserService
    {
        private readonly MainDbContext _context;
        public UserService(MainDbContext context)
        {
            _context = context;
        }
    }
}
