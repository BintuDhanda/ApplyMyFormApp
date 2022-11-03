using Microsoft.AspNetCore.Identity;
using rojgar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rojgar.Utilities
{
    public class CheckUser
    {
        private UserManager<ApplicationUser> _userManager;
        public CheckUser(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> ValidUser(string userId = null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (user.IsActive)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
