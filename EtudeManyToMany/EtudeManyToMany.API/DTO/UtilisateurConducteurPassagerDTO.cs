using EtudeManyToMany.Core.Model;

namespace EtudeManyToMany.API.DTO
{
    public class UtilisateurConducteurPassagerDTO
    {
        public Utilisateur Utilisateur { get; set; }
        public Conducteur Conducteur { get; set; }
        public Passager Passager { get; set; }
    }
}
