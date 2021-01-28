using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraDeConsumo
{
    public class clsPelicula
    {
        public int idPelicula;
        public string nombrePelicula;

        public clsPelicula(int id, string nombre)
        {
            idPelicula = id;
            nombrePelicula = nombre;
        }
    }
}
