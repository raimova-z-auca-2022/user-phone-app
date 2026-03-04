using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserPhoneApp.Data;
using UserPhoneApp.Models;

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
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(User user)
    {
        if (!ModelState.IsValid)
            return View(user);

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}