using BloodDonationUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(BloodDonationUI.Startup))]
namespace BloodDonationUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        // In this method we will create default User roles and Admin user for login   
        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Admin"
                };
                roleManager.Create(role);

                //Here we create an Admin super user who will maintain the website                  

                var user = new ApplicationUser();

                user.Email = "andras.straszner@gmail.com";
                user.UserName = "andras.straszner@gmail.com";
                string userPWD = "Admin_123";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Donor role    
            if (!roleManager.RoleExists("Donor"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Donor"
                };
                roleManager.Create(role);
            }
        }
    }
}
