namespace K14Forum.Migrations
{
    using K14Forum.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<K14Forum.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(K14Forum.Models.ApplicationDbContext context)
        {
            // Roles
            if (!context.Roles.Any(r => r.Name == "AppAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppAdmin" };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "AppMember"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "AppMember" };
                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "chuongnhh@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "chuongnhh@gmail.com",
                    FullName="Nguyễn Hoàng Chương",
                    Gender="Nam",
                    Email = "chuongnhh@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0164 7931 390",
                    PhoneNumberConfirmed = true,
                    Image = "/Images/default.png",
                    BirthDate = DateTime.Parse("1994/08/29")
                };

                manager.Create(user, "&14110013Hc");
                manager.AddToRole(user.Id, "AppAdmin");
            }

            // Tags
            //if (context.ApplicationTags.Any(x => x.Tag == "Asp.net"))
            //{
            //    var tag = new ApplicationTag
            //    {
            //        Tag = "Asp.net"
            //    };
            //    context.ApplicationTags.Add(tag);
            //}
            //if (context.ApplicationTags.Any(x => x.Tag == "Html"))
            //{
            //    var tag = new ApplicationTag
            //    {
            //        Tag = "Html"
            //    };
            //    context.ApplicationTags.Add(tag);
            //}
        }
    }
}
