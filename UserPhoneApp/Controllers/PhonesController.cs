using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UserPhoneApp.Data;
using UserPhoneApp.Models;

namespace UserPhoneApp.Controllers;

public class PhonesController : Controller
{
    private readonly AppDbContext _db;

    public PhonesController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var phones = await _db.Phones
            .AsNoTracking()
            .Include(p => p.User)
            .OrderBy(p => p.User!.Name)
            .ToListAsync();

        return View(phones);
    }

    public async Task<IActionResult> Create(int? userId = null)
    {
        await FillUsersDropDown(userId);
        return View(new Phone { UserId = userId ?? 0 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Phone phone)
    {
        if (!ModelState.IsValid)
        {
            await FillUsersDropDown(phone.UserId);
            return View(phone);
        }

        var userExists = await _db.Users.AnyAsync(u => u.Id == phone.UserId);
        if (!userExists)
        {
            ModelState.AddModelError(nameof(Phone.UserId), "Выберите существующего пользователя");
            await FillUsersDropDown(phone.UserId);
            return View(phone);
        }

        _db.Phones.Add(phone);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var phone = await _db.Phones.FindAsync(id);
        if (phone is null) return NotFound();

        await FillUsersDropDown(phone.UserId);
        return View(phone);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Phone phone)
    {
        if (id != phone.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            await FillUsersDropDown(phone.UserId);
            return View(phone);
        }

        var dbPhone = await _db.Phones.FindAsync(id);
        if (dbPhone is null) return NotFound();

        dbPhone.PhoneNumber = phone.PhoneNumber;
        dbPhone.UserId = phone.UserId;

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var phone = await _db.Phones
            .AsNoTracking()
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (phone is null) return NotFound();
        return View(phone);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var phone = await _db.Phones.FindAsync(id);
        if (phone is null) return NotFound();

        _db.Phones.Remove(phone);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task FillUsersDropDown(int? selectedUserId = null)
    {
        var users = await _db.Users
            .AsNoTracking()
            .OrderBy(u => u.Name)
            .ToListAsync();

        ViewBag.Users = new SelectList(users, "Id", "Name", selectedUserId);
    }
}