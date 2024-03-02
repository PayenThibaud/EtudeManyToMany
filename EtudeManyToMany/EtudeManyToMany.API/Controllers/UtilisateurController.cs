using EtudeManyToMany.API.Repository;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace EtudeManyToMany.API.Controllers
{
    [Route("api/[controller]")]
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
                if (u.Passager == null)
                {
                    u.Passager = _passagerRepository.Get(p => p.UtilisateurId == u.UtilisateurId).Result;
                }
                if (u.Conducteur == null)
                {
                    u.Conducteur = _conducteurRepository.Get(c => c.UtilisateurId == u.UtilisateurId).Result;
                }
            });

            return Ok(utilisateurs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ChercherUnUtilisateur(int id)
        {
            var utilisateur = await _utilisateurRepository.GetById(id);

            if (utilisateur != null)
            {
                if (utilisateur.Passager == null)
                {
                    utilisateur.Passager = await _passagerRepository.Get(p => p.UtilisateurId == utilisateur.UtilisateurId);
                }

                if (utilisateur.Conducteur == null)
                {
                    utilisateur.Conducteur = await _conducteurRepository.Get(c => c.UtilisateurId == utilisateur.UtilisateurId);
                }

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
    }
}
