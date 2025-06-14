using Microsoft.AspNetCore.Identity;


namespace QLHocPhan_23TH0003.ViewModel
{
    public class UserViewModel: IdentityUser
    {
        public List<string> Roles { get; set; }
    }
}
