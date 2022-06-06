using Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EducaGrado.Estudiante.Grades
{
    /// <summary>
    /// Lógica de interacción para StudentGrade.xaml
    /// </summary>
    public partial class StudentGrade : UserControl
    {
        GradeImpl gradeImpl;
        ModalityImpl modalityImpl;
        public StudentGrade()
        {
            InitializeComponent();
        }

        private void UserControl_Initialized(object sender, EventArgs e)
        {
            loadGrid();
        }
        public void loadGrid()
        {
            try
            {
                
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = crearData().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public DataTable crearData()
        {
            gradeImpl = new GradeImpl();
            modalityImpl = new ModalityImpl();
            DataTable grades = new DataTable();
            DataTable modality = new DataTable();
            DataTable gradetotal = new DataTable();
            int type = 0;
            int numbergrades = 0;
            int numbertest = 0;
            string typeq = "";
            modality = modalityImpl.Select();
            grades = gradeImpl.Select();
            gradetotal.Columns.Add(new DataColumn("Materia"));
            foreach (DataRow d in modality.Rows)
            {
                type = int.Parse(d[2].ToString());
                numbergrades = int.Parse(d[0].ToString());
                numbertest = int.Parse(d[1].ToString());
                typeq = d[3].ToString();
            }
            for (int i=1;i<=type;i++)
            {
                gradetotal.Columns.Add(new DataColumn(""+typeq+i));
            }
            gradetotal.Columns.Add(new DataColumn("PromedioTotal"));

            int count = 1;
            DataRow row1 = gradetotal.NewRow();
            double suma = 0;
            double promedio = 0;
            foreach (DataRow d in grades.Rows)
            {
                if (count==1)
                {
                    row1 = gradetotal.NewRow();
                    
                }
                row1["" + typeq + count] = "Promedio: "+ d[5].ToString().Substring(0, d[5].ToString().IndexOf(".")+2) + "\n"+"Practicas: "+ d[3].ToString().Substring(0, d[3].ToString().IndexOf(".") + 2) + "\n"+
                    "Examenes: "+ d[4].ToString().Substring(0, d[4].ToString().IndexOf(".") + 2);
                suma += Convert.ToDouble(d[5].ToString());
                if (count == type)
                {
                    promedio = suma / type;
                    row1["Materia"] = d[1].ToString();
                    row1["PromedioTotal"] = promedio;
                    gradetotal.Rows.Add(row1);
                    suma = 0;
                    count =0;
                }

                count++;
            }
            return gradetotal;

        }
    }
}
