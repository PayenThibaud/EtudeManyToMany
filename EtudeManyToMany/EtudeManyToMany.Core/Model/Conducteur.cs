using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtudeManyToMany.Core.Model
{
    public class Conducteur
    {
        public int ConducteurId { get; set; }

        // Conducteur est associé à un Utilisateur (relation One To One)
        public int UtilisateurId { get; set; }
        public Utilisateur? Utilisateur { get; set; }

        // Liste des trajets proposé par le conducteur (relation Many To One)
        public List<Trajet>? Trajets { get; set; }

        // Liste des commentaire reçu des passager (relation Many to Many)

        public List<Commentaire>? Commentaires { get; set;}
    }
}
