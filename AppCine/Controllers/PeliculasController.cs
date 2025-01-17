﻿using System;
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
    public class PeliculasController : Controller
    {
        private readonly AppCineDBContext _context;

        public PeliculasController(AppCineDBContext context)
        {
            _context = context;
        }

        [Authorize]

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {
              return _context.Peliculas != null ? 
                          View(await _context.Peliculas.ToListAsync()) :
                          Problem("Entity set 'AppCineDBContext.Peliculas'  is null.");
        }
       

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Idpelicula == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idpelicula,Titulo,Sipnosis,Director,Genero,Poster")] Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                pelicula.Idpelicula = ObtenerNuevoIdPelicula();

                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }
        public int ObtenerNuevoIdPelicula()
        {
            var maxIdPelicula = _context.Peliculas.Max(c => (int?)c.Idpelicula) ?? 0;

            return maxIdPelicula + 1;
        }


        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        
        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idpelicula,Titulo,Sipnosis,Director,Genero,Poster")] Pelicula pelicula)
        {
            if (id != pelicula.Idpelicula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Idpelicula))
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
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Idpelicula == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'AppCineDBContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Peliculas?.Any(e => e.Idpelicula == id)).GetValueOrDefault();
        }
    }
}
