using EtudeManyToMany.API.Repository;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtudeManyToMany.API.Controllers
{
    [Route("conducteurs")]
    [ApiController]
    public class ConducteurController : ControllerBase
    {
        private readonly IRepository<Conducteur> _conducteurRepository;
        private readonly IRepository<Trajet> _trajetRepository;
        private readonly IRepository<Utilisateur> _utilisateurRepository;

        public ConducteurController(IRepository<Conducteur> conducteurRepository, IRepository<Trajet> trajetRepository, IRepository<Utilisateur> utilisateurRepository)
        {
            _conducteurRepository = conducteurRepository;
            _trajetRepository = trajetRepository;
            _utilisateurRepository = utilisateurRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ToutLesConducteurs()
        {
            var conducteurs = await _conducteurRepository.GetAll();

            foreach (var conducteur in conducteurs)
            {
                conducteur.Utilisateur = await _utilisateurRepository.GetById(conducteur.UtilisateurId);

                conducteur.Trajets = await _trajetRepository.GetAll(t => t.ConducteurId == conducteur.ConducteurId);
            }

            return Ok(conducteurs);
        }



        [HttpGet("{conducteurId}")]
        public async Task<IActionResult> Get(int conducteurId)
        {
            var conducteur = await _conducteurRepository.GetById(conducteurId);

            if (conducteur == null)
            {
                return NotFound("Conducteur non trouvé");
            }

            conducteur.Utilisateur = await _utilisateurRepository.GetById(conducteur.UtilisateurId);

            conducteur.Trajets = await _trajetRepository.GetAll(t => t.ConducteurId == conducteur.ConducteurId);

            return Ok(conducteur);
        }


        [HttpPost("{utilisateurId}")]
        public async Task<IActionResult> AjoutConducteur(int utilisateurId, [FromBody] Conducteur conducteur)
        {
            var utilisateur = await _utilisateurRepository.GetById(utilisateurId);
            if (utilisateur == null)
                return BadRequest("Utilisateur non trouvé");

            var dejaConducteur = await _conducteurRepository.GetById(utilisateurId);
            if (dejaConducteur != null)
                return BadRequest("L'utilisateur à déjà un ConducteurId");

            utilisateur.Conducteur = conducteur;

            await _utilisateurRepository.Update(utilisateur);

            return Ok("Conducteur ajouté avec succès et associé à l'utilisateur");
        }

        [HttpDelete("Retrait-du-Conducteur/{utilisateurId}/{conducteurId}")]
        public async Task<IActionResult> RetraitConducteur(int utilisateurId, int conducteurId)
        {
            if (await _utilisateurRepository.GetById(utilisateurId) == null)
                return BadRequest("Utilisateur introuvable");

            var ing = await _conducteurRepository.GetById(conducteurId);

            if (ing == null)
                return BadRequest("Conducteur introuvable");

            if (ing.UtilisateurId != utilisateurId)
                return BadRequest("Conducteur est sur un autre utilisateur");

            if (await _conducteurRepository.Delete(ing.ConducteurId))
                return Ok("Conducteur retirer");

            return BadRequest("Oh oh ... des problèmes");
        }
    }
}
