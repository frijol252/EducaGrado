using EducaGrado.xDialog;
using Implementation;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace EducaGrado.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para TeacherModif.xaml
    /// </summary>
    public partial class TeacherModif : Window
    {
        int idPerson;
        #region parameters
        string usuario;
        Person person;
        TeacherImpl teacherimpl;
        UserAccount usuarioclass;
        BitmapImage image;
        int idTown = -1;
        double latitude = 0, longitude = 0;
        TeacherList teacherList;
        #endregion;
        public TeacherModif(int idPerson, TeacherList teacherList)
        {
            
            InitializeComponent();
            this.idPerson = idPerson;
            this.teacherList = teacherList;
            aniadirDatos();
        }

        #region controlgridup
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region aniadirDatos
        public void aniadirDatos()
        {
            try
            {
                teacherimpl = new TeacherImpl();
                person = teacherimpl.Get(idPerson);
                txtname.Text = person.Names;
                txtlastname.Text = person.LastName;
                txtsecondlastname.Text = person.SecondLastName;
                txtemail.Text = person.Email;
                txtPhone.Text = person.Phone;
                txtAddress.Text = person.Address;
                txtBirth.Text = person.BirthDate.ToString();
                txtCi.Text = person.Ci;
                txtCieX.Text = person.Ciextension;
                txtGender.SelectedItem = person.Gender;

                image = ToImage(person.Photo);
                imagesector.Source = image;

                Location ubi = new Location(person.Latitude, person.Longitude);
                MyMap.Center = ubi;
                ubicationPoint = ubi;
                Pushpin point = new Pushpin();
                point.Location = ubi;
                MyMap.Children.Clear();
                MyMap.Children.Add(point);
                latitude = ubicationPoint.Latitude;
                longitude = ubicationPoint.Longitude;


                CargarCombos(person.TownId, person.SchoolId);
                idTown = person.TownId;
            }
            catch (Exception ex)
            {
                MsgBox.Show("Comuniquese con el soporte de Educa" + ex.Message, "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
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
            }

        }
        #endregion
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

        #region combos

        CityImpl cityImpl;
        ProvinceImpl provinceImpl;
        TownImpl townImpl;


        public void CargarCombos(int idTown, int idProvince)
        {
            try
            {
                cityImpl = new CityImpl();
                DataTable ciudad = cityImpl.Select();
                comboCiudad.ItemsSource = ciudad.DefaultView;
                comboCiudad.SelectedIndex = 0;

                provinceImpl = new ProvinceImpl();
                DataTable province = provinceImpl.Select(int.Parse(comboCiudad.SelectedValue.ToString()));
                comboProvincia.ItemsSource = province.DefaultView;
                comboProvincia.SelectedIndex = idProvince;

                townImpl = new TownImpl();
                DataTable town = townImpl.Select(idProvince);
                comboMuni.ItemsSource = town.DefaultView;
                comboMuni.SelectedValue = idTown;
            }
            catch
            {
                MsgBox.Show("Comunicate con el equipo de Educa ", "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
            }
        }

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


        public void comboTown()
        {
            townImpl = new TownImpl();
            DataTable town = townImpl.Select(int.Parse(comboProvincia.SelectedValue.ToString()));
            comboMuni.ItemsSource = town.DefaultView;
            comboMuni.SelectedIndex = 0;
        }


        private void ComboMuni_DropDownClosed(object sender, EventArgs e)
        {
            idTown = int.Parse(comboMuni.SelectedValue.ToString());
            MsgBox.Show("Seleccionado " + comboMuni.Text, "Atencion", MsgBox.Buttons.OK);
        }

        #endregion

        private void InsertNow_Click_1(object sender, RoutedEventArgs e)
        {
            if (validar())
            {
                if (validarMapa())
                {
                    if (validarCombos())
                    {
                        System.Windows.Forms.DialogResult result = MsgBox.Show("Esta seguro de Modificar a " + person.Names + " " + person.LastName + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            person = new Person(txtname.Text, txtlastname.Text, txtsecondlastname.Text, txtAddress.Text
                                , txtCi.Text, txtCieX.Text, DateTime.Parse(txtBirth.Text), ToByte(image), txtemail.Text, latitude,
                                longitude, txtPhone.Text, txtGender.Text, idTown);
                            person.PersonId = idPerson;
                            teacherimpl = new TeacherImpl();
                            teacherimpl.UpdateTransact(person);
                            MsgBox.Show("Profesor Modificado", "Completado", MsgBox.Buttons.OK, MsgBox.Icon.Info);
                            teacherList.loadGrid();
                            this.Close();
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
                                            return true;
            return false;
        }
        public bool validarCombos()
        {
            if (idTown != -1)
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

        
    }
}
