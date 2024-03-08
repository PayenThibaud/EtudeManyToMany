using EtudeManyToMany.Blazor.Services;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EtudeManyToMany.Blazor.Pages
{
    public partial class TrajetPage : ComponentBase
    {
        private Trajet? Trajet { get; set; }

        [Parameter]
        public int ConducteurId { get; set; }

        [Inject]
        private IService<Trajet> TrajetService { get; set; }

        private void AddTrajet()
        {
            Trajet = new Trajet();
        }

        private async Task SubmitTrajet()
        {
            if (Trajet != null)
            {
                if (ConducteurId == 0)
                {
                    return;
                }

                Trajet.ConducteurId = ConducteurId;

                bool success = await TrajetService.Add(Trajet);
                if (success)
                {
                    Trajet = null;
                }
            }
        }
    }
}
