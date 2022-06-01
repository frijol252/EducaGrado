using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Implementation;
using Model;
using System.Data;
using Microsoft.Win32;
using System.IO;
using EducaGrado.xDialog;
using System.Security.Cryptography;

namespace EducaGrado.Administrativo.Students
{
    /// <summary>
    /// Lógica de interacción para StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        int idcourse;
        int idcity=-1, idprovince=-1, idtown=-1;
        double latitude=0, longitude=0;
        public StudentAdd(int i)
        {
            this.idcourse = i;
            InitializeComponent();
            comboCity();
        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            loadImage();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        BitmapImage image;
        Student stu;
        StudentImpl studentImpl;
        Person person;
        string pathImagePortada = null;
        byte[] imagebyte;

        private void InsertNow_Click_1(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                if (validarMapa())
                {
                    if (validarCombos())
                    {
                        System.Windows.Forms.DialogResult result = MsgBox.Show("Esta seguro de Agregar a " + txtname.Text + " "+txtlastname.Text + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            stu = new Student(0, idcourse, txtrude.Text);
                            person = new Person(txtname.Text, txtlastname.Text, txtsecondlastname.Text, txtAddress.Text
                                , txtCi.Text, txtCieX.Text, DateTime.Parse(txtBirth.Text), ToByte(image), txtemail.Text, latitude,
                                longitude, txtPhone.Text, txtGender.Text, idtown);
                            studentImpl = new StudentImpl();
                            studentImpl.InsertTransact(stu, person);
                        }
                    }
                    else
                    {
                        MsgBox.Show("Seleccione un pueblo", "Atencion", MsgBox.Buttons.OK);
                    }
                }
                else
                {
                    MsgBox.Show("Seleccione un punto en el mapa con doble click", "Atencion", MsgBox.Buttons.OK);
                }
            }
            else
            {
                MsgBox.Show("Llene todos los campos obligatorios", "Atencion", MsgBox.Buttons.OK);
            }
        }
        public bool validar()
        {
            if (!string.IsNullOrEmpty(txtname.Text))
                if (!string.IsNullOrEmpty(txtlastname.Text))
                    if (!string.IsNullOrEmpty(txtemail.Text))
                        if (!string.IsNullOrEmpty(txtPhone.Text))
                            if (!string.IsNullOrEmpty(txtCi.Text))
                                    if (!string.IsNullOrEmpty(txtBirth.Text))
                                        if (!string.IsNullOrEmpty(txtAddress.Text))
                                            if (!string.IsNullOrEmpty(txtrude.Text))
                                                return true;
            return false;
        }
        public bool validarCombos()
        {
            if (idtown != -1)
               return true;
            return false;
        }
        public bool validarMapa()
        {
            if (latitude != 0)
                if (longitude != 0)
                    return true;
            return false;
        }
        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (ofd.ShowDialog() == true)
            {
                image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = new Uri(ofd.FileName);
                image.EndInit();
                imagesector.Source = image;
                //pathImagePortada = ofd.FileName;
                File.ReadAllBytes("");
            }

        }
        
        public void loadImage()
        {
            image = ToImage(File.ReadAllBytes(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\images\Test.txt")));
            imagesector.Source = image;
        }
        public byte[] ToByte(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
                return data;
            }
        }


        public BitmapImage ToImage(byte[] array)
        {
            using (MemoryStream ms = new MemoryStream(array))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;//CacheOption must be set after BeginInit()
                img.StreamSource = ms;
                img.EndInit();

                if (img.CanFreeze)
                {
                    img.Freeze();
                }


                return img;
            }
        }

        #region mapa
        Location ubicationPoint;
        private void MyMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                e.Handled = true;
                var mousePosicion = e.GetPosition((UIElement)sender);
                ubicationPoint = MyMap.ViewportPointToLocation(mousePosicion);
                Pushpin point = new Pushpin();
                point.Location = ubicationPoint;
                MyMap.Children.Clear();
                MyMap.Children.Add(point);
                latitude = ubicationPoint.Latitude;
                longitude = ubicationPoint.Longitude;
            }
            catch
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }
        }

        private void btnSaltelite_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Focus();
            MyMap.Mode = new AerialMode(true);
        }

        private void btnCalles_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Focus();
            MyMap.Mode = new RoadMode();
        }

        private void btnZoom_Click(object sender, RoutedEventArgs e)
        {
            MyMap.Focus();
            MyMap.ZoomLevel++;
        }

        private void btnAlejar_Click(object sender, RoutedEventArgs e)
        {
            MyMap.ZoomLevel--;
        }
        #endregion




        #region Combos

        CityImpl cityImpl;
        public void comboCity()
        {
            try
            {
                cityImpl = new CityImpl();
                DataTable ciudad = cityImpl.Select();
                comboCiudad.ItemsSource = ciudad.DefaultView;
                comboCiudad.SelectedIndex = 0;
                comboProvince();
            }
            catch
            {
                MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            }

        }

        private void ComboCiudad_DropDownClosed(object sender, EventArgs e)
        {
            
            comboProvince();

        }
        ProvinceImpl provinceImpl;
        public void comboProvince()
        {
            provinceImpl = new ProvinceImpl();
            DataTable province = provinceImpl.Select(int.Parse(comboCiudad.SelectedValue.ToString()));
            comboProvincia.ItemsSource = province.DefaultView;
            comboProvincia.SelectedIndex = 0;
            comboTown();
        }
        
        private void ComboProvincia_DropDownClosed(object sender, EventArgs e)
        {
            
            comboTown();
        }
        TownImpl townImpl;
        Town town;
        public void comboTown()
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.Select(int.Parse(comboProvincia.SelectedValue.ToString()));
            comboMuni.ItemsSource = town.DefaultView;
            comboMuni.SelectedIndex = 0;
        }
        

        private void ComboMuni_DropDownClosed(object sender, EventArgs e)
        {
            idtown = int.Parse(comboMuni.SelectedValue.ToString());
            MsgBox.Show("Seleccionado "+comboMuni.Text, "Atencion", MsgBox.Buttons.OK);
        }









        #endregion

       

    }
}
