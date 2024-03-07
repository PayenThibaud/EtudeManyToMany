using EtudeManyToMany.API.Repository;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace EtudeManyToMany.API.Controllers
{
    [Route("[controller]")]
    [ApiController] // Ajoutez cet attribut
    public class UtilisateurController : ControllerBase
    {
        private readonly IRepository<Utilisateur> _utilisateurRepository;
        private readonly IRepository<Passager> _passagerRepository;
        private readonly IRepository<Conducteur> _conducteurRepository;

        public UtilisateurController(IRepository<Utilisateur> utilisateurRepository, IRepository<Passager> passagerRepository, IRepository<Conducteur> conducteurRepository)
        {
            _utilisateurRepository = utilisateurRepository;
            _passagerRepository = passagerRepository;
            _conducteurRepository = conducteurRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ToutLesUtilisateurs()
        {
            var utilisateurs = await _utilisateurRepository.GetAll();

            utilisateurs.ForEach(u =>
            {
                    u.Passager = _passagerRepository.Get(p => p.UtilisateurId == u.UtilisateurId).Result;
                    
                    u.Conducteur = _conducteurRepository.Get(c => c.UtilisateurId == u.UtilisateurId).Result;
            });

            return Ok(utilisateurs);
        }



        [HttpGet("{utilisateurId}")]
        public async Task<IActionResult> ChercherUnUtilisateur(int utilisateurId)
        {
            var utilisateur = await _utilisateurRepository.GetById(utilisateurId);

            if (utilisateur != null)
            {

                    utilisateur.Passager = await _passagerRepository.Get(p => p.UtilisateurId == utilisateur.UtilisateurId);

                    utilisateur.Conducteur = await _conducteurRepository.Get(c => c.UtilisateurId == utilisateur.UtilisateurId);

                return Ok(utilisateur);
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> AjoutUtilisateur([FromBody] Utilisateur utilisateur)
        {
            var utilisateurId = await _utilisateurRepository.Add(utilisateur);

            if (utilisateurId > 0)
                return CreatedAtAction(nameof(ToutLesUtilisateurs), "Utilisateur ajouter");

            return BadRequest("Oh oh ... des problèmes");
        }



        [HttpPost("ajout-du-Conducteur/{utilisateurId}")]
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



        [HttpPost("ajout-du-Passager/{utilisateurId}")]
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



        [HttpPut("{utilisateurId}")]
        public async Task<IActionResult> UpdateUtilisateur(int utilisateurId, [FromBody] Utilisateur utilisateur)
        {
            var util = await _utilisateurRepository.GetById(utilisateurId);
            if (util == null)
                return BadRequest("Utilisateur introuvable");

            utilisateur.UtilisateurId = utilisateurId;
            if (await _utilisateurRepository.Update(utilisateur))
                return Ok("Utilisateur mis à jours");

            return BadRequest("Oh oh ... des problèmes");
        }



        [HttpDelete("{utilisateurId}")]
        public async Task<IActionResult> RetraitUtilisateur(int utilisateurId)
        {
            var util = await _utilisateurRepository.GetById(utilisateurId);
            if (util == null)
                return BadRequest("Utilisateur introuvable");

            if (await _utilisateurRepository.Delete(utilisateurId))
                return Ok("Utilisateur retirer");

            return BadRequest("Oh oh ... des problèmes");
        }


    }
}
