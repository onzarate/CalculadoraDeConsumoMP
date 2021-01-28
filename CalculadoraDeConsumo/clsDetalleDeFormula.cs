using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraDeConsumo
{
    public class clsDetalleDeFormula
    {
        public int idDF;
        public int fkFormula;
        public int fkMP;
        public Double porcentaje;

        public clsDetalleDeFormula(int id, int fkform, int fkmp, Double porc)
        {
            idDF = id;
            fkFormula = fkform;
            fkMP = fkmp;
            porcentaje = porc;
        }
    }
}
