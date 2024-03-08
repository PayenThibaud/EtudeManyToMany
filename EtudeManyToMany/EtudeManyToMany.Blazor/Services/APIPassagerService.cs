using System.Linq.Expressions;
using System.Net.Http.Json;
using EtudeManyToMany.Core.Model;

namespace EtudeManyToMany.Blazor.Services
{
    public class APIPassagerService : IService<Passager>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiRoute; // = "http://localhost:7044/passagers";

        public APIPassagerService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiRoute = configuration["ManyAPIUrlHttps"] + "/passagers";
        }

        public async Task<bool> Add(Passager passager)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute, passager);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync(_baseApiRoute + $"/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<Passager?> Get(Expression<Func<Passager, bool>> predicate)
        {
            var passagers = await GetAll(); // Obtenez tous les passagers
            return passagers.FirstOrDefault(predicate.Compile()); // Appliquez le prédicat sur la liste de passagers
        }

        public async Task<List<Passager>> GetAll()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Passager>>(_baseApiRoute);
            return result!;
        }

        public async Task<List<Passager>> GetAll(Expression<Func<Passager, bool>> predicate)
        {
            var passagers = await GetAll(); // Obtenez tous les passagers
            return passagers.Where(predicate.Compile()).ToList(); // Appliquez le prédicat sur la liste de passagers
        }

        public async Task<Passager?> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Passager    >(_baseApiRoute + $"/{id}");
            return result;
        }

        public async Task<bool> Update(Passager passager)
        {
            var result = await _httpClient.PutAsJsonAsync(_baseApiRoute + $"/{passager.PassagerId}", passager);
            return result.IsSuccessStatusCode;
        }
    }
}
