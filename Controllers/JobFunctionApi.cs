using MedEquipCentral.Models;
using Microsoft.EntityFrameworkCore;
namespace MedEquipCentral.Controllers
{
    public class JobFunctionApi
    {
        public static void Map(WebApplication app)
        {

            app.MapGet("/users/jobFunction", (MedEquipCentralDbContext db) =>
            {
                return db.JobFunctions.ToList();
            });
        }
    }
}
