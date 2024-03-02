using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtudeManyToMany.Core.Model
{
    public class Administrateur
    {
        public int AdministrateurId { get; set; }
        public string Nom {  get; set; }
        public string Password { get; set; }
    }
}
