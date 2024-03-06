using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtudeManyToMany.Core.Model
{
    public class Trajet
    {
        public int TrajetId {  get; set; }
        public string LieuDepart { get; set;}
        public string LieuArrivee { get; set;}


        // Foreign key pour référencer le conducteur du trajet (relatation One To Many)
        public int ConducteurId { get; set; }

        // Trajet associé à un Conducteur (Many to One)
        public Conducteur? Conducteur { get; set; }

        // Liste de réservations pour ce trajet (relation Many To Many)
        public List<Reservation>? Reservations { get; set; }
    }
}
