using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtudeManyToMany.Core.Model
{
    public class Reservation
    {
        //Table de jointure entre Trajet et Passager (Relation Many to Many) 

        // Foreign key pour le trajet
        public int TrajetId { get; set; }
        public Trajet? Trajet { get; set; }

        // Foreign key pour le passager
        public int PassagerId { get; set; }
        public Passager? Passager { get; set; }
    }
}
