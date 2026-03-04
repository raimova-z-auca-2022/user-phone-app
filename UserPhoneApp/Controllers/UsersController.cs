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
    
    public async Task<IActionResult> Details(int id)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) return NotFound();

        return View(user);
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        return View(user);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, User user)
    {
        if (id != user.Id) return BadRequest();

        if (!ModelState.IsValid)
            return View(user);

        var dbUser = await _db.Users.FindAsync(id);
        if (dbUser is null) return NotFound();

        dbUser.Name = user.Name;
        dbUser.Email = user.Email;
        dbUser.DateOfBirth = user.DateOfBirth;

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user is null) return NotFound();

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _db.Users.FindAsync(id);
        if (user is null) return NotFound();

        _db.Users.Remove(user);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

}