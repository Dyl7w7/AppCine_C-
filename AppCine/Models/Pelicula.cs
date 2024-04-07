using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCine.Models
{
    public partial class Pelicula
    {
        public Pelicula()
        {
            Calificacions = new HashSet<Calificacion>();
        }
        [Key]
        public int Idpelicula { get; set; }
        public string? Titulo { get; set; }
        public string? Sipnosis { get; set; }
        public string? Director { get; set; }
        public string? Genero { get; set; }
        public string? Poster { get; set; }

        public virtual ICollection<Calificacion> Calificacions { get; set; }
    }
}
