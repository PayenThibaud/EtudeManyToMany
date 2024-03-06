using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtudeManyToMany.Core.Model
{
    public class Passager
    {
        public int PassagerId { get; set; }

        // Un passager est associé à un Utilisateur (Relation One To One)
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur {  get; set; }

        // Liste de réservations par le passagé (relation Many To Many)
        public List<Reservation>? Reservations { get; set; }
    }
}
