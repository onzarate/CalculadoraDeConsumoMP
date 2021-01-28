using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraDeConsumo
{
    public class clsMateriaPrima
    {
        public int idMP;
        public string nombreMP;

        public clsMateriaPrima(int id, string nombre)
        {
            idMP = id;
            nombreMP = nombre;
        }
    }
}
