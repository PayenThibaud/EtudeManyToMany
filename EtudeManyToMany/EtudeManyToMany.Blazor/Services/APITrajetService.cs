using EtudeManyToMany.Core.Model;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace EtudeManyToMany.Blazor.Services
{
    public class APITrajetService : IService<Trajet>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiRoute; // = "http://localhost:7044/api/trajets";

        public APITrajetService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiRoute = configuration["TrajetAPIUrlHttps"] + "/api/trajets";
        }
        public async Task<bool> Add(Trajet trajet)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute, trajet);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync(_baseApiRoute + $"/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<Trajet?> Get(Expression<Func<Trajet, bool>> predicate)
        {
            var trajets = await GetAll(); // Obtenez tous les passagers
            return trajets.FirstOrDefault(predicate.Compile()); // Appliquez le prédicat sur la liste de passagers
        }

        public async Task<List<Trajet>> GetAll()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Trajet>>(_baseApiRoute);
            return result!;
        }

        public async Task<List<Trajet>> GetAll(Expression<Func<Trajet, bool>> predicate)
        {
            var trajets = await GetAll(); // Obtenez tous les passagers
            return trajets.Where(predicate.Compile()).ToList(); // Appliquez le prédicat sur la liste de passagers
        }

        public async Task<Trajet?> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Trajet>(_baseApiRoute + $"/{id}");
            return result;
        }

        public async Task<bool> Update(Trajet trajet)
        {
            var result = await _httpClient.PutAsJsonAsync(_baseApiRoute + $"/{trajet.TrajetId}", trajet);
            return result.IsSuccessStatusCode;
        }
    }
}
