using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProRunners
{
    public class Paciente
    {

        public string Nombre { get; set; }
        public DateTime Nacimiento { get; set; }
        public int Edad
        {
            get
            {
                return DateTime.Today.AddTicks(-Nacimiento.Ticks).Year - 1;
            }
        }
        public List<DateTime> SesionesVideo { get; set; }
        public List<DateTime> SesionesFoto { get; set; }
        public string FullPath { get; set; }       

    }
}