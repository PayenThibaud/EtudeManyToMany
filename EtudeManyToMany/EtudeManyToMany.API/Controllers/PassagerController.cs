using EtudeManyToMany.API.Repository;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtudeManyToMany.API.Controllers
{
    [Route("passagers")]
    [ApiController]
    public class PassagerController : ControllerBase
    {
        private readonly IRepository<Passager> _passagerRepository;
        private readonly IRepository<Reservation> _reservationRepository;
        private readonly IRepository<Utilisateur> _utilisateurRepository;
        private readonly IRepository<Trajet> _trajetRepository;
        private readonly IRepository<Commentaire> _commentaireRepository;
        private readonly IRepository<Conducteur> _conducteurRepository;

        public PassagerController(IRepository<Passager> passagerRepository, IRepository<Reservation> reservationRepository, IRepository<Utilisateur> utilisateurRepository, IRepository<Trajet> trajetRepository, IRepository<Commentaire> commentaireRepository, IRepository<Conducteur> conducteurRepository)
        {
            _passagerRepository = passagerRepository;
            _reservationRepository = reservationRepository;
            _utilisateurRepository = utilisateurRepository;
            _trajetRepository = trajetRepository;
            _commentaireRepository = commentaireRepository;
            _conducteurRepository = conducteurRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ToutLesPassagers()
        {
            var passagers = await _passagerRepository.GetAll();

            foreach (var passager in passagers)
            {
                passager.Utilisateur = await _utilisateurRepository.GetById(passager.UtilisateurId);

                passager.Reservations = await _reservationRepository.GetAll(t => t.PassagerId == passager.PassagerId);
            }

            return Ok(passagers);
        }



        [HttpGet("{passagerId}")]
        public async Task<IActionResult> Get(int passagerId)
        {
            var passager = await _passagerRepository.GetById(passagerId);

            if (passager == null)
            {
                return NotFound("Passager introuvable");
            }

            passager.Utilisateur = await _utilisateurRepository.GetById(passager.UtilisateurId);

            passager.Reservations = await _reservationRepository.GetAll(r => r.PassagerId == passager.PassagerId);

            return Ok(passager);
        }

        [HttpPost("{utilisateurId}")]
        public async Task<IActionResult> AjoutPassager(int utilisateurId, [FromBody] Passager passager)
        {
            var utilisateur = await _utilisateurRepository.GetById(utilisateurId);
            if (utilisateur == null)
                return BadRequest("Utilisateur non trouvé");

            var dejaPassager = await _passagerRepository.GetById(utilisateurId);
            if (dejaPassager != null)
                return BadRequest("L'utilisateur à déjà un ConducteurId");

            utilisateur.Passager = passager;

            await _utilisateurRepository.Update(utilisateur);

            return Ok("Passager ajouté avec succès et associé à l'utilisateur");
        }


        [HttpDelete("Retrait-du-Passager/{utilisateurId}/{passagerId}")]
        public async Task<IActionResult> RetraitPassager(int utilisateurId, int passagerId)
        {
            if (await _utilisateurRepository.GetById(utilisateurId) == null)
                return BadRequest("Utilisateur introuvable");

            var ing = await _passagerRepository.GetById(passagerId);

            if (ing == null)
                return BadRequest("Passager introuvable");

            if (ing.UtilisateurId != utilisateurId)
                return BadRequest("Passager est sur un autre utilisateur");

            if (await _passagerRepository.Delete(ing.PassagerId))
                return Ok("Passager retirer");

            return BadRequest("Oh oh ... des problèmes");
        }
    }
}
