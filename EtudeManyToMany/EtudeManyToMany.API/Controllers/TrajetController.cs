using EtudeManyToMany.API.Repository;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EtudeManyToMany.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrajetController : ControllerBase
    {
        private readonly IRepository<Trajet> _trajetRepository;
        private readonly IRepository<Reservation> _reservationRepository;

        public TrajetController(IRepository<Trajet> trajetRepository, IRepository<Reservation> reservationRepository)
        {
            _trajetRepository = trajetRepository;
            _reservationRepository = reservationRepository;
        }



        [HttpGet]
        public async Task<IActionResult> ToutLesTrajets()
        {
            var trajets = await _trajetRepository.GetAll();

            foreach (var trajet in trajets)
            {
                    trajet.Reservations = await _reservationRepository.GetAll(r => r.TrajetId == trajet.TrajetId);
            }

            return Ok(trajets);
        }



        [HttpGet("{trajetId}")]
        public async Task<IActionResult> Get(int trajetId)
        {
            return Ok(await _trajetRepository.GetById(trajetId));
        }



    }
}
