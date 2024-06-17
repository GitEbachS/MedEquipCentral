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
                var user = db.Users.Where(u => u.Uid == uid).FirstOrDefault();

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
                    Uid = newUserDto.Uid,
                    IsAdmin = false
                };

                db.Users.Add(newUser);
                db.SaveChanges();

                return Results.Created($"/users/{newUser.Id}", newUser);
            });

            //update User
            app.MapPut("/updateUser/{userId}", (MedEquipCentralDbContext db, int userId, UpdateUserDto updatedUserDto) =>
            {
                var userToUpdate = db.Users.Find(userId);

                if (userToUpdate == null)
                {
                    return Results.NotFound();     //  return a 404 Not Found response
                }

                userToUpdate.FirstName = updatedUserDto.FirstName;
                userToUpdate.LastName = updatedUserDto.LastName;
                userToUpdate.Image = updatedUserDto.Image;
                userToUpdate.Email = updatedUserDto.Email;
                userToUpdate.Address = updatedUserDto.Address;
                userToUpdate.JobFunctionId = updatedUserDto.JobFunctionId;
                userToUpdate.IsBizOwner = updatedUserDto.IsBizOwner;

                db.SaveChanges();

                // Return a 200 OK response with the updated user details in the response body
                return Results.Ok(userToUpdate);
            });

            //get single user's details
            app.MapGet("/singleUser/{userId}", (MedEquipCentralDbContext db, int userId) =>
            {
                var singleUser = db.Users
                .Include(u => u.JobFunction)
                .Where(u => u.Id == userId)
                .Select(u => new
                {   
                    u.Id,
                    u.FirstName,
                    u.LastName, 
                    u.Image, 
                    u.Email,
                    u.Address,
                    u.JobFunctionId,
                    JobFunction = new {u.JobFunction.Id, u.JobFunction.Name },
                    u.IsBizOwner,
                    u.IsAdmin
                })

                .SingleOrDefault();
                if (singleUser == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(singleUser);
            });
        }
    }
}
