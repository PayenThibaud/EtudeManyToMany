using EtudeManyToMany.Blazor;
using EtudeManyToMany.Blazor.Services;
using EtudeManyToMany.Core.Model;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IService<Utilisateur>, APIUtilisateurService>();
builder.Services.AddScoped<IService<Conducteur>, APIConducteurService>();
builder.Services.AddScoped<IService<Passager>, APIPassagerService>();
builder.Services.AddScoped<IService<Trajet>, APITrajetService>();
builder.Services.AddScoped<IService<Reservation>, APIReservationService>();

await builder.Build().RunAsync();
