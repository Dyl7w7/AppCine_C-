using AppCine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace AppCine.Controllers
{
    public class IndexCine : Controller
    {

        private readonly AppCineDBContext _context;
        private readonly ILogger<AppCineDBContext> _logger;

        public IndexCine(ILogger<AppCineDBContext> logger, AppCineDBContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index(int? page, string busca)
        {
            int pageNumber = page ?? 1;
            int pageSize = 6;
            if (busca != null)
            {
                var pelicula = from peliculaBusca in _context.Peliculas select peliculaBusca; 
                
                pelicula = pelicula.Where(c => c.Titulo!.Contains(busca));
                return View(pelicula.ToPagedList(pageNumber, pageSize));
                
            }
            else
            {
                

                var peliculas = _context.Peliculas.ToPagedList(pageNumber, pageSize);

                return View(peliculas);
            }
            

        }

        [HttpPost]
        public async Task<IActionResult> CalificarPelicula(Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                calificacion.Idcalificacion = ObtenerNuevoIdCalificacion();

                _context.Calificacions.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound(calificacion);
            }
        }

        public int ObtenerNuevoIdCalificacion()
        {            
            var maxIdCalificacion = _context.Calificacions.Max(c => (int?)c.Idcalificacion) ?? 0;

            return maxIdCalificacion + 1;
        }

    }
}
