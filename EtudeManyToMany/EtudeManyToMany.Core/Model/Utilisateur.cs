using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtudeManyToMany.Core.Model
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }
        public string Nom {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string? Photo { get; set; }
        public string? NomVehicule { get; set; }
        public string? Description { get; set; }

        public bool isAdmin { get; set; } = false;
        // L'utilisateur peut être un passager (Relation One To One)
        public Passager? Passager { get; set; }
        // L'utilisateur peut être un conducteur (Relation One To One)
        public Conducteur? Conducteur { get; set; }

    }
}
