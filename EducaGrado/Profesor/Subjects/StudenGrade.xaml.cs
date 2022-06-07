using EducaGrado.xDialog;
using Implementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EducaGrado.Profesor.Subjects
{
    /// <summary>
    /// Lógica de interacción para StudenGrade.xaml
    /// </summary>
    public partial class StudenGrade : Window
    {
        int Typeclass;
        int classid;
        GradeImpl gradeImpl;
        ModalityImpl modalityImpl;
        int numbergrades = 0;
        int numbertest = 0;
        List<int> list = new List<int>();
        List<double> listgrades = new List<double>();
        public StudenGrade(int Typeclass, int classid)
        {
            InitializeComponent();
            this.Typeclass = Typeclass;
            this.classid = classid;
            loadGrid();
           
        }

        private void btnAddGrade_Click(object sender, RoutedEventArgs e)
        {
            listgrades = crearLista();
            MessageBox.Show("" + listgrades.Count + " " + list.Count);
            gradeImpl.UpdateTransact(list, listgrades);
            loadGrid2();
            MsgBox.Show("Notas Actualizadas", "Completado", MsgBox.Buttons.OK, MsgBox.Icon.Info);
            dataRow();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        public void loadGrid()
        {
            try
            {
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = cargardata().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void loadGrid2()
        {
            try
            {
                
                dgvDatos.ItemsSource = null;
                dgvDatos.ItemsSource = cargardata2().DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public DataTable cargardata()
        {
            gradeImpl = new GradeImpl();
            modalityImpl = new ModalityImpl();
            DataTable grades = new DataTable();
            DataTable modality = new DataTable();
            DataTable gradetotal = new DataTable();
            
            modality = modalityImpl.Select();
            grades = gradeImpl.SelectTeacher(classid,Typeclass);
            gradetotal.Columns.Add(new DataColumn("ID"));
            gradetotal.Columns.Add(new DataColumn("Name"));
            foreach (DataRow d in modality.Rows)
            {
                numbergrades = int.Parse(d[0].ToString());
                numbertest = int.Parse(d[1].ToString());
            }
            for (int i = 1; i <= numbergrades; i++)
            {
                dgvDatos.Columns.Add(CreateTextBoxColumn("Practica" + i));
                gradetotal.Columns.Add(new DataColumn("Practica" + i));
            }
            for (int i = 1; i <= numbertest; i++)
            {
                dgvDatos.Columns.Add(CreateTextBoxColumn("Examen" + i));
                gradetotal.Columns.Add(new DataColumn("Examen" + i));
            }
            
            int count = 1;
            DataRow row1 = gradetotal.NewRow();
            foreach (DataRow d in grades.Rows)
            {
                if (count == 1)
                {
                    row1 = gradetotal.NewRow();

                }
                if (count>=1 && count <=numbergrades)
                {
                    
                    row1["Practica"+count] = d[1].ToString();
                    list.Add(int.Parse(d[2].ToString()));

                }
                if(count>numbergrades && count<= (numbergrades + numbertest))
                {
                    row1["Examen" + (count-numbergrades)] = d[1].ToString();
                    list.Add(int.Parse(d[2].ToString()));
                }
                if (count== (numbergrades + numbertest))
                {
                    row1["ID"]= d[0].ToString();
                    row1["Name"] = d[3].ToString();
                    gradetotal.Rows.Add(row1);
                    count = 0;
                }
                
                count++;
            }
            
            return gradetotal;
        }
        public DataTable cargardata2()
        {
            gradeImpl = new GradeImpl();
            modalityImpl = new ModalityImpl();
            DataTable grades = new DataTable();
            DataTable modality = new DataTable();
            DataTable gradetotal = new DataTable();

            modality = modalityImpl.Select();
            grades = gradeImpl.SelectTeacher(classid, Typeclass);
            gradetotal.Columns.Add(new DataColumn("ID"));
            gradetotal.Columns.Add(new DataColumn("Name"));
            for (int i = 1; i <= numbergrades; i++)
            {
                gradetotal.Columns.Add(new DataColumn("Practica" + i));
            }
            for (int i = 1; i <= numbertest; i++)
            {
                gradetotal.Columns.Add(new DataColumn("Examen" + i));
            }
            foreach (DataRow d in modality.Rows)
            {
                numbergrades = int.Parse(d[0].ToString());
                numbertest = int.Parse(d[1].ToString());
            }

            int count = 1;
            DataRow row1 = gradetotal.NewRow();
            foreach (DataRow d in grades.Rows)
            {
                if (count == 1)
                {
                    row1 = gradetotal.NewRow();

                }
                if (count >= 1 && count <= numbergrades)
                {

                    row1["Practica" + count] = d[1].ToString();
                    list.Add(int.Parse(d[2].ToString()));

                }
                if (count > numbergrades && count <= (numbergrades + numbertest))
                {
                    row1["Examen" + (count - numbergrades)] = d[1].ToString();
                    list.Add(int.Parse(d[2].ToString()));
                }
                if (count == (numbergrades + numbertest))
                {
                    row1["ID"] = d[0].ToString();
                    row1["Name"] = d[3].ToString();
                    gradetotal.Rows.Add(row1);
                    count = 0;
                }

                count++;
            }

            return gradetotal;
        }
        private DataGridTemplateColumn CreateTextBoxColumn(string header)
        {
            var col = new DataGridTemplateColumn();
            col.Header = header;
            var template = new DataTemplate();
            var textBlockFactory = new FrameworkElementFactory(typeof(TextBox));
            Binding binding = new Binding();
            binding.Path = new PropertyPath(""+ header);
            binding.Mode = BindingMode.TwoWay;
            textBlockFactory.Name = "txtSection";
            textBlockFactory.SetBinding(TextBox.TextProperty, binding);
            textBlockFactory.SetValue(TextBox.WidthProperty, 50.0);
            template.VisualTree = textBlockFactory;

            col.CellTemplate = template;

            return col;
        }
        private void dataRow()
        {

            int columns = dgvDatos.Columns.Count;
            
            int rows = dgvDatos.Items.Count;
            for (int i=0;i < rows; i++)
            {
                for(int j = 2; j< (2+numbergrades+numbertest);j++)
                {
                    ContentPresenter myCp = dgvDatos.Columns[j].GetCellContent(dgvDatos.Items[i]) as ContentPresenter;
                    var myTemplate = myCp.ContentTemplate;
                    TextBox mytxtbox = myTemplate.FindName("txtSection", myCp) as TextBox;
                    double nota = Convert.ToDouble(mytxtbox.Text);
                    if (nota > 0)
                    {
                        mytxtbox.IsEnabled = false;
                    }

                }
            }
           

        }
        private List<double> crearLista()
        {
            List<double> listadouble = new List<double>();
            int columns = dgvDatos.Columns.Count;

            int rows = dgvDatos.Items.Count;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 2; j < (2 + numbergrades + numbertest); j++)
                {
                    ContentPresenter myCp = dgvDatos.Columns[j].GetCellContent(dgvDatos.Items[i]) as ContentPresenter;
                    var myTemplate = myCp.ContentTemplate;
                    TextBox mytxtbox = myTemplate.FindName("txtSection", myCp) as TextBox;
                    double nota = Convert.ToDouble(mytxtbox.Text);
                    listadouble.Add(nota);
                    
                }
            }
            MessageBox.Show("" + listadouble[0] + " " + listadouble[6]);

            return listadouble;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataRow();
        }
    }
}
