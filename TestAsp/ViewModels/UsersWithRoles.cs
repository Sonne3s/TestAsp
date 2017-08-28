using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestAsp.DAL;
using TestAsp.Models;

namespace TestAsp.ViewModels
{
    public class UsersWithRoles
    {
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string Role { get; set; }
        public string UserID { get; set; }
        public UsersWithRoles(EFDbContext context)
        {
            //var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            //var userManager=HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            Users = context.Users.ToList();
            Roles = context.Roles.ToList();
        }
        public UsersWithRoles() { }
    }
}