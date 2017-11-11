using System.Collections.Generic;

namespace AutomotiveShop.model.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AutomotiveShop.model.AutomotiveShopDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AutomotiveShop.model.AutomotiveShopDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            foreach (var category in context.Categories)
            {
                context.Categories.Remove(category);
            }
            var categories = new List<Category>()
            {
                new Category() {Name = "Brakes & Brake Parts"},
                new Category() {Name = "Service Kits"}
            };

            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            foreach (var subcategory in context.Subcategories)
            {
                context.Subcategories.Remove(subcategory);
            }
            var subcategories = new List<Subcategory>()
            {
                new Subcategory() {Name = "Brake Discs", Category = context.Categories.FirstOrDefault(c => c.Name == "Brakes & Brake Parts")},
                new Subcategory() {Name = "Brake Pads", Category = context.Categories.FirstOrDefault(c => c.Name == "Brakes & Brake Parts")},
                new Subcategory() {Name = "Engine Cooling", Category = context.Categories.FirstOrDefault(c => c.Name == "Service Kits") },
                new Subcategory() {Name = "Suspension & Steering", Category = context.Categories.FirstOrDefault(c => c.Name == "Service Kits") }
            };

            subcategories.ForEach(s => context.Subcategories.Add(s));
            context.SaveChanges();

            foreach (var product in context.Products)
            {
                context.Products.Remove(product);
            }
            var products = new List<Product>()
            {
                new Product() {Name = "GENUINE BREMBO INTERNALLY VENTED FRONT BRAKE DISCS 09.5674.21 - Ø 276 mm", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Brake Discs"), Price = 10.99, ItemsAvailable = 2},
                new Product() {Name = "Corsa VXR Front & Rear Dimpled Grooved Brake Discs & Mintex Pads", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Brake Discs"), Price = 24.99, ItemsAvailable = 2},
                new Product() {Name = "GENUINE ASTRA ZAFIRA VECTRA SIGNUM WATER PUMP PIPE 1.9 8v DIESEL ENGINE 93194989", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Engine Cooling"), Price = 5.99, ItemsAvailable = 2},
                new Product() {Name = "2x Rear Replacement Gas Pressure Strut OE Quality Suspension Shock Absorber", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Suspension & Steering"), Price = 9.99, ItemsAvailable = 2},
                new Product() {Name = "NEW FRONT LEFT OR RIGHT SUSPENSION TRACK CONTROL ARM BUSH 9443882", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Suspension & Steering"), Price = 99.99, ItemsAvailable = 2},
                new Product() {Name = "1 x Genuine Range Rover Sport 2002-2013 Rear Suspension Toe Link LR019117", Subcategory = context.Subcategories.FirstOrDefault(c => c.Name == "Suspension & Steering"), Price = 449.99, ItemsAvailable = 2}
            };

            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();




            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            string password = "P@ssw0rd";
            foreach (var user in context.Users)
            {
                context.Users.Remove(user);
            }
            var admin = new ApplicationUser()
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };

            var usr = new ApplicationUser()
            {
                UserName = "user@user.com",
                Email = "user@user.com"
            };

            var createResult1 = userManager.Create(admin, password);
            var createResult2 = userManager.Create(usr, password);

            var roles = new List<string>
            {
                "Administrator",
                "User"
            };

            foreach (var role in roles)
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole(role));
                }
            }

            if (createResult1.Succeeded)
            {
                userManager.AddToRole(admin.Id, roles[0]);
            }

            if (createResult2.Succeeded)
            {
                userManager.AddToRole(usr.Id, roles[1]);
            }

            foreach (var address in context.DeliveryAddresses)
            {
                context.DeliveryAddresses.Remove(address);
            }

            var addresses = new List<DeliveryAddress>()
            {
                new DeliveryAddress() {DeliveryAddressId = Guid.NewGuid(), City = "City", StreetName = "Street", Postcode = "55-555", UserId = admin.Id },
                new DeliveryAddress() {DeliveryAddressId = Guid.NewGuid(), City = "City2", StreetName = "Street2", Postcode = "66-666", UserId = admin.Id }

            };
            addresses.ForEach(a => context.DeliveryAddresses.Add(a));
            context.SaveChanges();
        }
    }
}
