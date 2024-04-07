using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCine.Models;
using Microsoft.AspNetCore.Authorization;

namespace AppCine.Controllers
{
    public class CalificacionsController : Controller
    {
        private readonly AppCineDBContext _context;


        public CalificacionsController(AppCineDBContext context)
        {
            _context = context;
        }

        [Authorize]

        // GET: Calificacions
        public async Task<IActionResult> Index()
        {
            var appCineDBContext = _context.Calificacions.Include(c => c.IdpeliculaNavigation);
            return View(await appCineDBContext.ToListAsync());
        }

        // GET: Calificacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Calificacions == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacions
                .Include(c => c.IdpeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Idcalificacion == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

      


        // GET: Calificacions/Create
        public IActionResult Create()
        {
            ViewData["Idpelicula"] = new SelectList(_context.Peliculas, "Idpelicula", "Idpelicula");
            return View();
        }

        // POST: Calificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcalificacion,Idpelicula,Calificacion1")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idpelicula"] = new SelectList(_context.Peliculas, "Idpelicula", "Idpelicula", calificacion.Idpelicula);
            return View(calificacion);
        }

        // GET: Calificacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Calificacions == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacions.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            ViewData["Idpelicula"] = new SelectList(_context.Peliculas, "Idpelicula", "Idpelicula", calificacion.Idpelicula);
            return View(calificacion);
        }

        // POST: Calificacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idcalificacion,Idpelicula,Calificacion1")] Calificacion calificacion)
        {
            if (id != calificacion.Idcalificacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.Idcalificacion))
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
            ViewData["Idpelicula"] = new SelectList(_context.Peliculas, "Idpelicula", "Idpelicula", calificacion.Idpelicula);
            return View(calificacion);
        }

        // GET: Calificacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Calificacions == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacions
                .Include(c => c.IdpeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Idcalificacion == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: Calificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Calificacions == null)
            {
                return Problem("Entity set 'AppCineDBContext.Calificacions'  is null.");
            }
            var calificacion = await _context.Calificacions.FindAsync(id);
            if (calificacion != null)
            {
                _context.Calificacions.Remove(calificacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(int id)
        {
          return (_context.Calificacions?.Any(e => e.Idcalificacion == id)).GetValueOrDefault();
        }
    }
}
