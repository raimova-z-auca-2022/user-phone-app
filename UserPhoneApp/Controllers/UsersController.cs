using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPhoneApp.Data;

namespace UserPhoneApp.Controllers;

public class UsersController : Controller
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _db.Users
            .AsNoTracking()
            .OrderBy(u => u.Name)
            .ToListAsync();

        return View(users);
    }
}