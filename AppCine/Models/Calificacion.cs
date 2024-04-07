using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCine.Models
{
    public partial class Calificacion
    {
        [Key]
        public int Idcalificacion { get; set; }
        public int? Idpelicula { get; set; }
        public int? Calificacion1 { get; set; }

        public virtual Pelicula? IdpeliculaNavigation { get; set; }
    }
}
