using CodeProject.StoreDB.Model.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeProject.StoreDB.Portal.Startup))]
namespace CodeProject.StoreDB.Portal
{    

    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// The Configuration
        /// </summary>
        /// <param name="app">The app<see cref="IAppBuilder"/></param>
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login
        /// <summary>
        /// The createRolesandUsers
        /// </summary>
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("Admin"))
            {
                // first we create Admin rool
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website

                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";

                string userPWD = "abcde.A1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }
            else
            {
                //Here we create a Admin super user who will maintain the website

                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";

                string userPWD = "abcde.A1";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            // creating Creating Employee role
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }

            // Create customer:

            var customer = new ApplicationUser();
            customer.UserName = "customer@gmail.com";
            customer.Email = "customer@gmail.com";

            string pwd = "abcde.A1";

            var result = UserManager.Create(customer, pwd);

            
        }
    }
}
