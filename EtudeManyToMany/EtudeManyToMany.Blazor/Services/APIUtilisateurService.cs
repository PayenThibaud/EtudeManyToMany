using EtudeManyToMany.Core.Model;
using System.Linq.Expressions;
using System.Net.Http.Json;

namespace EtudeManyToMany.Blazor.Services
{
    public class APIUtilisateurService : IService<Utilisateur>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiRoute; // = "http://localhost:7044/api/utilisateurs";

        public APIUtilisateurService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiRoute = configuration["UtilisateurAPIUrlHttps"] + "/api/utilisateurs";
        }
        public async Task<bool> Add(Utilisateur utilisateur)
        {
            var result = await _httpClient.PostAsJsonAsync(_baseApiRoute, utilisateur);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync(_baseApiRoute + $"/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<Utilisateur?> Get(Expression<Func<Utilisateur, bool>> predicate)
        {
            var passagers = await GetAll(); // Obtenez tous les passagers
            return passagers.FirstOrDefault(predicate.Compile()); // Appliquez le prédicat sur la liste de passagers
        }

        public async Task<List<Utilisateur>> GetAll()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Utilisateur>>(_baseApiRoute);
            return result!;
        }

        public async Task<List<Utilisateur>> GetAll(Expression<Func<Utilisateur, bool>> predicate)
        {
            var utilisateurs = await GetAll(); // Obtenez tous les passagers
            return utilisateurs.Where(predicate.Compile()).ToList(); // Appliquez le prédicat sur la liste de passagers
        }


        public async Task<Utilisateur?> GetById(int id)
        {
            var result = await _httpClient.GetFromJsonAsync<Utilisateur>(_baseApiRoute + $"/{id}");
            return result;
        }

        public async Task<bool> Update(Utilisateur utilisateur)
        {
            var result = await _httpClient.PutAsJsonAsync(_baseApiRoute + $"/{utilisateur.UtilisateurId}", utilisateur);
            return result.IsSuccessStatusCode;
        }
    }
}
