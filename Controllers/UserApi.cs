using MedEquipCentral.Models;
using MedEquipCentral.DTO;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class UserApi
    {
        public static void Map(WebApplication app)
        {
            //check user's uid in the database
            app.MapPost("/checkUser/{uid}", (MedEquipCentralDbContext db, string uid) =>
            {
                var user = db.Users.Where(u => u.Uid == uid).ToList();

                if (user == null)
                {
                    return Results.NotFound("User not registered");
                }

                return Results.Ok(user);
            });

            //register user
            app.MapPost("/registerUser", (MedEquipCentralDbContext db, UserDto newUserDto) =>
            {
                // Check if the email is already registered
                var existingUser = db.Users.FirstOrDefault(u => u.Email == newUserDto.Email);
                if (existingUser != null)
                {
                    return Results.Conflict("Email already registered");
                }

                // Create a new User entity from the provided DTO
                var newUser = new User
                {
                    FirstName = newUserDto.FirstName,
                    LastName = newUserDto.LastName,
                    Image = newUserDto.Image,
                    Email = newUserDto.Email,
                    Address = newUserDto.Address,
                    JobFunctionId = newUserDto.JobFunctionId,
                    IsBizOwner = newUserDto.IsBizOwner,
                    Uid = existingUser.Uid,
                    IsAdmin = false
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                return Results.Created($"/users/{newUser.Id}", newUser);
            });
        }
    }
    }
}
