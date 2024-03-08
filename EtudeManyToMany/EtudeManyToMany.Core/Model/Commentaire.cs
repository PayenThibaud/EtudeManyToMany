using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtudeManyToMany.Core.Model
{
    public class Commentaire
    {
        //Table de jointure entre Passager et Conducteur (Relation Many to Many) 
        public int CommentaireId { get; set; }
        public int Note { get; set; }
        public string Avis {  get; set; }

        // Foreign key pour le passager
        public int PassagerId { get; set; }
        public Passager? Passager { get; set; }

        // Foreign key pour le conducteur
        public int ConducteurId { get; set; }
        public Conducteur? Conducteur { get; set; }
    }
}
