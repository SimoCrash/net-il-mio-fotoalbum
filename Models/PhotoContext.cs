using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PhotoDB;Integrated Security=True;Pooling=False;Encrypt=False");
        }

        

        public void Seed()
        {

            var photoSeed = new Photo[]
            {
                new Photo
                {
                    Title = "Foto tramonto",
                    Description = "Che bel tramonto",
                    ImageUrl = "https://picsum.photos/200/300",
                    Visible = true,
                },
                new Photo
                {
                    Title = "Foto alba",
                    Description = "Che bell'alba",
                    ImageUrl = "https://picsum.photos/200/300",
                    Visible = true,
                },
                new Photo
                {
                    Title = "Foto mezzoggiorno",
                    Description = "Che bel mezzoggiorno",
                    ImageUrl = "https://picsum.photos/200/300",
                    Visible = true,
                },
            };

            if (!Photos.Any())
            {
                Photos.AddRange(photoSeed);
            };


            var CategoriesSeed = new Category[]
            {
                new Category
                {
                    Name = "Generica",
                    Photos = photoSeed.ToList(),
                },
                new Category
                {
                    Name = "Natura",
                },
                new Category
                {
                    Name = "Italia",
                },
            };

            if (!Categories.Any())
            {
                Categories.AddRange(CategoriesSeed);
            }

            if (!Roles.Any())
            {
                var RolesSeed = new IdentityRole[]
                {
                    new("ADMIN"),
                    new("USER"),
                };

                Roles.AddRange(RolesSeed);
            }

            if (Users.Any(u => u.Email == "admin@dev.com" || u.Email == "user@dev.com") && !UserRoles.Any())
            {
                var admin = Users.First(u => u.Email == "admin@dev.com");
                var user = Users.First(u => u.Email == "user@dev.com");

                var adminRole = Roles.First(r => r.Name == "ADMIN");
                var userRole = Roles.First(r => r.Name == "USER");

                var UserRoleSeed = new IdentityUserRole<string>[]
                {
                    new()
                    {
                        UserId = admin.Id,
                        RoleId = adminRole.Id,
                        //password = zarate
                    },
                    new()
                    {
                        UserId = user.Id,
                        RoleId = userRole.Id,
                        //password = zarate
                    }
                };
                UserRoles.AddRange(UserRoleSeed);
            }

            SaveChanges();
        }

    }
}
