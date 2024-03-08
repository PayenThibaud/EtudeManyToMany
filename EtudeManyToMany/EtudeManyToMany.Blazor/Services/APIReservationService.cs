using EtudeManyToMany.Core.Model;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace EtudeManyToMany.Blazor.Services
{
    public class APIReservationService : IService<Reservation>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiRoute; // = "http://localhost:7044/passagers";

        public APIReservationService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiRoute = configuration["ManyAPIUrlHttps"] + "/passagers";
        }
        public async Task<bool> Add(Reservation reservation)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute, reservation);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync(_baseApiRoute + $"/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<Reservation?> Get(Expression<Func<Reservation, bool>> predicate)
        {
            var reservations = await GetAll(); // Obtenez tous les passagers
            return reservations.FirstOrDefault(predicate.Compile()); // Appliquez le prédicat sur la liste de passagers
        }

        public async Task<List<Reservation>> GetAll()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Reservation>>(_baseApiRoute);
            return result!;
        }

        public async Task<List<Reservation>> GetAll(Expression<Func<Reservation, bool>> predicate)
        {
            var reservations = await GetAll(); // Obtenez tous les passagers
            return reservations.Where(predicate.Compile()).ToList(); // Appliquez le prédicat sur la liste de passagers
        }

        public async Task<Reservation?> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Reservation>(_baseApiRoute + $"/{id}");
            return result;
        }

        public async Task<bool> Update(Reservation reservation)
        {
            var result = await _httpClient.PutAsJsonAsync(_baseApiRoute + $"/{reservation.PassagerId}", reservation);
            return result.IsSuccessStatusCode;
        }
    }
}
