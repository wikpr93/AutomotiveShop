using AutomotiveShop.model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutomotiveShop.service.Service
{
    public class UserService
    {
        private AutomotiveShopDbContext _dbContext = new AutomotiveShopDbContext();

        public ApplicationUser ReturnUserByUsername(string name)
        {
            if (name == "")
            {
                return null;
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dbContext));
            ApplicationUser userToReturn = userManager.FindByName(name);
            return (userToReturn);
        }
    }
}