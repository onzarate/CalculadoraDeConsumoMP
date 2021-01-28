using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraDeConsumo
{
    public class clsFormula
    {
        public int idFormula;
        public int fkPelicula;
        public string nombreFormula;

        public clsFormula(int id, int fkpel, string nombre)
        {
            idFormula = id;
            fkPelicula = fkpel;
            nombreFormula = nombre;
        }
    }
}
