using System;
using System.Collections.Generic;

#nullable disable

namespace Sakila.Models
{
    public partial class FilmActor
    {
        public short ActorId { get; set; }
        public short FilmId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Film Film { get; set; }
    }
}
