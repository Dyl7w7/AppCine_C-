using AppCine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AppCine.Controllers
{
    public class IndexPuntaje : Controller
    {
        private readonly AppCineDBContext _context;

        public IndexPuntaje(AppCineDBContext context)
        {
            _context = context;
        }

        [Authorize]

        public IActionResult Index()
        {
            
            var resultados = _context.Peliculas
                .Join(_context.Calificacions,
                      p => p.Idpelicula,
                      c => c.Idpelicula,
                      (p, c) => new { Pelicula = p, Calificacion = c })
                .GroupBy(pc => pc.Pelicula.Titulo)
                .Select(g => new Grafica
                {
                    Pelicula = g.Key,
                    CalificacionPromedio = (double)g.Average(pc => pc.Calificacion.Calificacion1)
                })
                .OrderByDescending(pc => pc.CalificacionPromedio)
                .Take(5)
                .ToList();

            // Puedes pasar 'resultados' a la vista
            return View(resultados);
        }
    }
}
