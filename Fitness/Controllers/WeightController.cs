using Fitness.Data;
using Fitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Controllers;

public class WeightController : Controller
{
    private readonly AppDb _context;
    
    public WeightController(AppDb context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var data = await _context.WeightMeasurements.OrderByDescending(x => x.Date).ToListAsync();
        return View(data);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(WeightMeasurement weightMeasurement)
    {
        if (!ModelState.IsValid)
        {
            return View(weightMeasurement);
        }
        _context.Add(weightMeasurement);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var weightMeasurement = await _context.WeightMeasurements.FindAsync(id);
        if (weightMeasurement == null)
        {
            return NotFound();
        }
        return View(weightMeasurement);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, WeightMeasurement weightMeasurement)
    {
        if (id != weightMeasurement.Id)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(weightMeasurement);
        }
        try
        {
            _context.Update(weightMeasurement);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!Exists(weightMeasurement.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var weightMeasurement = await _context.WeightMeasurements.FindAsync(id);
        if (weightMeasurement != null)
        {
            _context.WeightMeasurements.Remove(weightMeasurement);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
    
    private bool Exists(int id)
    {
        return _context.WeightMeasurements.Any(e => e.Id == id);
    }
}