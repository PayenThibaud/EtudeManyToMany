﻿using EtudeManyToMany.API.Repository;
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

        [HttpPost("Ajout-Reservation/{passagerId}/{trajetId}")]
        public async Task<IActionResult> AjoutReservation(int passagerId, int trajetId, [FromBody] Reservation reservation)
        {
            var passager = await _passagerRepository.GetById(passagerId);
            if (passager == null)
                return BadRequest("Passager introuvable");

            var trajet = await _trajetRepository.GetById(trajetId);
            if (trajet == null)
                return BadRequest("Trajet introuvable");

            var dejaReservation = await _reservationRepository.Get(r => r.PassagerId == passagerId && r.TrajetId == trajetId);
            if (dejaReservation != null)
                return BadRequest("Le passager a déjà une réservation pour ce trajet");

            reservation.PassagerId = passagerId;
            reservation.TrajetId = trajetId;

            await _reservationRepository.Add(reservation);

            return Ok("Le passager à fait sa reservation !");
        }


        [HttpDelete("Retrait-Reservation/{passagerId}/{reservationId}")]
        public async Task<IActionResult> RetraitReservation(int passagerId, int reservationId)
        {
            if (await _passagerRepository.GetById(passagerId) == null)
                return BadRequest("Passager introuvable");

            var ing = await _reservationRepository.GetById(reservationId);

            if (ing == null)
                return BadRequest("Reservation introuvable");

            if (ing.PassagerId != passagerId)
                return BadRequest("La reservation est sur un autre passager");

            if (await _reservationRepository.Delete(ing.ReservationId))
                return Ok("Reservation retirer");

            return BadRequest("Oh oh ... des problèmes");
        }

        [HttpPost("Ajout-Commentaire/{passagerId}/{conducteurId}")]
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
