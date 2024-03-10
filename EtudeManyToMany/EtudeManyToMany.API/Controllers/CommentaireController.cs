using EtudeManyToMany.API.Repository;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtudeManyToMany.API.Controllers
{
    [Route("commentaires")]
    [ApiController]
    public class CommentaireController : ControllerBase
    {
        private readonly IRepository<Commentaire> _commentaireRepository;
        private readonly IRepository<Passager> _passagerRepository;
        private readonly IRepository<Conducteur> _conducteurRepository;

        public CommentaireController(IRepository<Commentaire> commentaireRepository, IRepository<Passager> passagerRepository, IRepository<Conducteur> conducteurRepository)
        {
            _commentaireRepository = commentaireRepository;
            _passagerRepository = passagerRepository;
            _conducteurRepository = conducteurRepository;
        }

        [HttpGet]
        public async Task<IActionResult> TouteLesCommentaires()
        {
            var reservations = await _commentaireRepository.GetAll();

            return Ok(reservations);
        }


        [HttpGet("{commentaireId}")]
        public async Task<IActionResult> Get(int commentaireId)
        {
            var reservations = await _commentaireRepository.GetById(commentaireId);

            return Ok(reservations);
        }


        [HttpPost("{passagerId}/{conducteurId}")]
        public async Task<IActionResult> AjoutReservation(int passagerId, int conducteurId, [FromBody] Commentaire commentaire)
        {
            var passager = await _passagerRepository.GetById(passagerId);
            if (passager == null)
                return BadRequest("Passager introuvable");

            var conducteur = await _conducteurRepository.GetById(conducteurId);
            if (conducteur == null)
                return BadRequest("Conducteur introuvable");

            var dejaCommentaire = await _commentaireRepository.Get(r => r.PassagerId == passagerId && r.ConducteurId == conducteurId);
            if (dejaCommentaire != null)
                return BadRequest("Le passager a déjà fait un commentaire pour ce conducteur");

            commentaire.PassagerId = passagerId;
            commentaire.ConducteurId = conducteurId;

            await _commentaireRepository.Add(commentaire);

            return Ok("Le passager à fait son commentaire !");
        }
    }
}
