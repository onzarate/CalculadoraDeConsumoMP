using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraDeConsumo
{
    public partial class frmCalculadoraDeConsumo : Form
    {
        //Calculador de consumos
        //El objetivo es calcular el consumo de materia prima, segun las peliculas que se vayan a fabricar, pudiendo elegir entre diferentes formulas
        // y pudiendo elegir la eficiencia esperada
        //Para facilitar la ejecución en distintas PC, no utilizo BD, genero por código nombres genericos de peliculas, materias primas y formulas, 
        //los cuales guardo en listas, que "simulan" las tablas

        //listas donde guardo las peliculas, mps, formulas y detalles de formulas 
        private List<clsPelicula> peliculas = new List<clsPelicula>();
        private List<clsMateriaPrima> materiasPrimas = new List<clsMateriaPrima>();
        private List<clsFormula> formulas = new List<clsFormula>();
        private List<clsDetalleDeFormula> detalles = new List<clsDetalleDeFormula>();
        
        private List<int> idFormulas = new List<int>(); //lista auxiliar para guardar id de formulas que muestro en cmbFormulas

        private double totalAgregado = 0;//acumulador de cantidades agregadas al calculo

        public frmCalculadoraDeConsumo()
        {
            InitializeComponent();
            crearElementos();
            cargarComboPeliculas();
        } 
        private void crearElementos()//genero nombres genericos de peliculas, mps, formulas y detalles, y los guardo en las listas
        {
            for (int i=0; i<3; i++)
            {
                peliculas.Add(new clsPelicula(i + 1, "Película " + (i + 1)));
            }

            for (int i=0; i<3; i++)
            {
                materiasPrimas.Add (new clsMateriaPrima(i+1, "Materia prima " + (i+1)));
            }

            int indice = 1;
            for (int i=0; i<3; i++)
            {
                for(int j=0; j<3; j++)
                {
                    formulas.Add(new clsFormula(indice, j+1, "Fórmula " + indice + " película " + (j+1)));
                    indice++;
                }
            }

            detalles.Add(new clsDetalleDeFormula(1, 1, 1, 0.2));
            detalles.Add(new clsDetalleDeFormula(2, 1, 2, 0.45));
            detalles.Add(new clsDetalleDeFormula(3, 1, 3, 0.35));

            detalles.Add(new clsDetalleDeFormula(4, 2, 1, 0.3));
            detalles.Add(new clsDetalleDeFormula(5, 2, 2, 0.35));
            detalles.Add(new clsDetalleDeFormula(6, 2, 3, 0.35));

            detalles.Add(new clsDetalleDeFormula(7, 3, 1, 0.4));
            detalles.Add(new clsDetalleDeFormula(8, 3, 2, 0.25));
            detalles.Add(new clsDetalleDeFormula(9, 3, 3, 0.35));

            detalles.Add(new clsDetalleDeFormula(10, 4, 1, 0.5));
            detalles.Add(new clsDetalleDeFormula(11, 4, 2, 0.25));
            detalles.Add(new clsDetalleDeFormula(12, 4, 3, 0.25));

            detalles.Add(new clsDetalleDeFormula(13, 5, 1, 0.35));
            detalles.Add(new clsDetalleDeFormula(14, 5, 2, 0.35));
            detalles.Add(new clsDetalleDeFormula(15, 5, 3, 0.30));

            detalles.Add(new clsDetalleDeFormula(16, 6, 1, 0.25));
            detalles.Add(new clsDetalleDeFormula(17, 6, 2, 0.25));
            detalles.Add(new clsDetalleDeFormula(18, 6, 3, 0.50));

            detalles.Add(new clsDetalleDeFormula(19, 7, 1, 0.15));
            detalles.Add(new clsDetalleDeFormula(20, 7, 2, 0.55));
            detalles.Add(new clsDetalleDeFormula(21, 7, 3, 0.30));

            detalles.Add(new clsDetalleDeFormula(22, 8, 1, 0.45));
            detalles.Add(new clsDetalleDeFormula(23, 8, 2, 0.45));
            detalles.Add(new clsDetalleDeFormula(24, 8, 3, 0.10));

            detalles.Add(new clsDetalleDeFormula(25, 9, 1, 0.05));
            detalles.Add(new clsDetalleDeFormula(26, 9, 2, 0.35));
            detalles.Add(new clsDetalleDeFormula(27, 9, 3, 0.60));

        }

        private void cargarComboPeliculas()//Se cargan las peliculas en el cmbPelicula
        {
            for(int i = 0;i<peliculas.Count;i++)
            {
                cmbPeliculas.Items.Add(peliculas[i].nombrePelicula);
            }
        }

        private void cargarComboFormulas()//Se cargan las formulas en el cmbFormulas, segun la pelicula seleccionada
        {
            cmbFormulas.Items.Clear();
            idFormulas.Clear();

            if (cmbPeliculas.SelectedIndex!=-1)
            {
                List<clsFormula> listaForm = buscarFormulasPorPelicula(cmbPeliculas.SelectedIndex+1);

                for(int i=0;i<listaForm.Count;i++)
                {
                    cmbFormulas.Items.Add(listaForm[i].nombreFormula);
                    idFormulas.Add(listaForm[i].idFormula);
                }
            }
        }

        private List<clsFormula> buscarFormulasPorPelicula(int idPel)//metodo auxiliar para obtener todas las formulas para una pelicula
        {
            List<clsFormula> listaForm = new List<clsFormula>();

            for(int i=0; i<formulas.Count;i++)
            {
                if(formulas[i].fkPelicula==idPel)
                {
                    listaForm.Add(formulas[i]);
                }
            }

            return listaForm;
        }
        //si usara BD, este metodo deberia ir en la clase clsFormula

        private void cmbPeliculas_SelectedIndexChanged(object sender, EventArgs e)//Cuando se selecciona una pelicula, se carga el combo de formulas
        {
            cargarComboFormulas();
            cmbFormulas.ResetText();
        }

        private void btnAgregar_Click(object sender, EventArgs e)//Cuando se apreta el boton, se verifica si se selecciono pelicula, formula y cantidad
        {
            if(cmbPeliculas.SelectedIndex!=-1)
            {
                if(cmbFormulas.SelectedIndex!=-1)
                {
                    if(numCantidad.Value>0)
                    {
                        agregarAlCalculo();
                    }
                }
            }
        }

        private void agregarAlCalculo()//metodo para agregar al dgv de peliculas para calcular consumo
        {
            dgvPeliculasAgregadas.Rows.Add();
            dgvPeliculasAgregadas.Rows[dgvPeliculasAgregadas.Rows.Count - 1].Cells[clmIdPelicula.Index].Value = cmbPeliculas.SelectedIndex;
            dgvPeliculasAgregadas.Rows[dgvPeliculasAgregadas.Rows.Count - 1].Cells[clmPelicula.Index].Value = cmbPeliculas.SelectedItem.ToString();
            dgvPeliculasAgregadas.Rows[dgvPeliculasAgregadas.Rows.Count - 1].Cells[clmIdFormula.Index].Value = idFormulas[cmbFormulas.SelectedIndex];
            dgvPeliculasAgregadas.Rows[dgvPeliculasAgregadas.Rows.Count - 1].Cells[clmFormula.Index].Value = cmbFormulas.SelectedItem.ToString();
            dgvPeliculasAgregadas.Rows[dgvPeliculasAgregadas.Rows.Count - 1].Cells[clmCantidad.Index].Value = numCantidad.Value;
            totalAgregado +=(double)numCantidad.Value;//actualizo el total agregado

            txtTotalAgregado.Text = totalAgregado.ToString();
            limpiarControles();
        }

        private void limpiarControles()//metodo para limpiar los combos despues de agregar
        {
            cmbPeliculas.SelectedIndex = -1;
            cmbPeliculas.ResetText();
            cmbFormulas.SelectedIndex = -1;
            cmbFormulas.ResetText();
            numCantidad.Value = 0;
        }

        private void dgvPeliculasAgregadas_CellClick(object sender, DataGridViewCellEventArgs e)//Al hacer clic en quitar se quita del calculo
        {
            if(e.RowIndex!=-1)
            {
                if(e.ColumnIndex==clmQuitar.Index)
                {
                    totalAgregado -= Convert.ToDouble(dgvPeliculasAgregadas.Rows[e.RowIndex].Cells[clmCantidad.Index].Value);
                    dgvPeliculasAgregadas.Rows.RemoveAt(e.RowIndex);

                    txtTotalAgregado.Text = totalAgregado.ToString();
                }
            }
        }

        private void btnEstimarConsumo_Click(object sender, EventArgs e)//metodo para estimar el consumo de Mp
        {
            List<int> mps = new List<int>();
            List<double> cant = new List<double>();
            List<clsDetalleDeFormula> detDeForm = new List<clsDetalleDeFormula>();
            double eficiencia = (double)numEficiencia.Value / 100;
            double total = 0;

            dgvConsumoEstimado.Rows.Clear();

            for(int i=0;i<dgvPeliculasAgregadas.Rows.Count;i++)//Recorro el dgv de peliculas agregadas
            {
                detDeForm.Clear();
                detDeForm = obtenerDetalleDeFormula(Convert.ToInt32(dgvPeliculasAgregadas.Rows[i].Cells[clmIdFormula.Index].Value));//para cada fila obtengo detalle de formula

                for(int j=0;j<detDeForm.Count;j++)//recorro la lista de detalle de formula obtenida
                {
                    if(!mps.Contains(detDeForm[j].fkMP))//si la MP no fue agregada antes, la agrego a lista de MPS para agregar
                    {
                        mps.Add(detDeForm[j].fkMP);
                        cant.Add(Math.Round((detDeForm[j].porcentaje * Convert.ToDouble(dgvPeliculasAgregadas.Rows[i].Cells[clmCantidad.Index].Value))/eficiencia,1));
                        //calculo la cantidad a consumir multiplicando el porcentaje por la cantidad y dividiendo por la eficiencia
                    }
                    else //si la MP ya fue agregada antes, solo la busco y actualizo la cantidad
                    {
                        for(int k=0;k<mps.Count;k++)
                        {
                            if(mps[k] == detDeForm[j].fkMP)
                            {
                                cant[k] += Math.Round((detDeForm[j].porcentaje * Convert.ToDouble(dgvPeliculasAgregadas.Rows[i].Cells[clmCantidad.Index].Value))/eficiencia,1);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < mps.Count; i++)//Una vez llena la lista de mps y la de cantidad, las recorro y completo el dgv de consumos
            {
                dgvConsumoEstimado.Rows.Add();
                dgvConsumoEstimado.Rows[dgvConsumoEstimado.Rows.Count - 1].Cells[clmIdMP.Index].Value = mps[i];
                dgvConsumoEstimado.Rows[dgvConsumoEstimado.Rows.Count - 1].Cells[clmMP.Index].Value = buscarMPporID(mps[i]).nombreMP;
                dgvConsumoEstimado.Rows[dgvConsumoEstimado.Rows.Count - 1].Cells[clmCantidadCons.Index].Value = cant[i];
                total += cant[i];
            }

            txtTotalAConsumir.Text = total.ToString();//Actualizo el total a consumir
        }

        private clsMateriaPrima buscarMPporID(int id)//metodo auxiliar para burcar MP por id de MP
        {
            for(int i=0;i<materiasPrimas.Count;i++)
            {
                if(id==materiasPrimas[i].idMP)
                {
                    return materiasPrimas[i];
                }
            }

            return new clsMateriaPrima(0,"");
        }
        //Si usara BD este metodo no va acá, seria un constructor en la clase clsMateriaPrima
        private List<clsDetalleDeFormula> obtenerDetalleDeFormula(int idform)//metodo para obtner el detalle de una formula mediante su id
        {
            List<clsDetalleDeFormula> lista = new List<clsDetalleDeFormula>();

            for(int i=0;i<detalles.Count;i++)
            {
                if(detalles[i].fkFormula==idform)
                {
                    lista.Add(detalles[i]);
                }
            }

            return lista;
        }
        //Si usara BD este metodo iria en la clase clsDetalleDeFormula, podria ser un metodo estatico
        private void btnVer_Click(object sender, EventArgs e)
        {
            if(cmbFormulas.SelectedIndex!=-1)
            {
                List<clsDetalleDeFormula> lista = obtenerDetalleDeFormula(idFormulas[cmbFormulas.SelectedIndex]);
                List<string> stringsmp = new List<string>();
                List<double> porcent = new List<double>();

                for(int i=0;i<lista.Count;i++)
                {
                    stringsmp.Add(buscarMPporID(lista[i].fkMP).nombreMP);
                    porcent.Add(lista[i].porcentaje*100);
                }
                frmVisualizadorDeFormula form = new frmVisualizadorDeFormula(cmbPeliculas.Text, cmbFormulas.Text, stringsmp, porcent);
                form.ShowDialog();
            }
        }//Boton para abrir el form de visualización de formula
    }
}
