using EducaGrado.xDialog;
using Implementation;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Model;
using System;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace EducaGrado.Administrativo.Teacher
{
    /// <summary>
    /// Lógica de interacción para TeacherAdd.xaml
    /// </summary>
    public partial class TeacherAdd : Window
    {
        #region parameters
        string usuario;
        Person person;
        TeacherImpl teacherimpl;
        UserAccount usuarioclass;
        BitmapImage image;
        int idTown = -1;
        double latitude = 0, longitude = 0;
        #endregion;
        public TeacherAdd()
        {
            InitializeComponent();
        }

        #region view;
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void modif()
        {
            Insestack.Height = 0;
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        Random ran = new Random();
        public void UsuarioAniadir()
        {
            if (txtsecondlastname.Text != null)
            {
                usuario = "" + txtlastname.Text.Substring(0, 1) + txtsecondlastname.Text.Substring(0, 1) + txtname.Text.Substring(0, 1) + DateTime.Now.ToString().Substring(6, 4) + ran.Next(100, 1000);
            }
            else
            {
                usuario = "" + txtlastname.Text.Substring(0, 1) + txtlastname.Text.Substring(txtlastname.MaxLength - 1, 1) + txtname.Text.Substring(0, 1) + DateTime.Now.ToString().Substring(6, 4) + ran.Next(100, 1000);
            }

        }
        Random rdn = new Random();

        private void InsertNow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validar())
                {
                    if (validarMapa())
                    {
                        if (validarCombos())
                        {
                            System.Windows.Forms.DialogResult result = MsgBox.Show("Esta seguro de Agregar a " + txtname.Text + " " + txtlastname.Text + "?", "Atencion", MsgBox.Buttons.YesNo, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                person = new Person(txtname.Text, txtlastname.Text, txtsecondlastname.Text, txtAddress.Text
                                    , txtCi.Text, txtCieX.Text, DateTime.Parse(txtBirth.Text), ToByte(image), txtemail.Text, latitude,
                                    longitude, txtPhone.Text, txtGender.Text, idTown);
                                teacherimpl = new TeacherImpl();
                                teacherimpl.InsertTransact(person);
                                MsgBox.Show("Profesor Insertado", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Exclamation, MsgBox.AnimateStyle.FadeIn);
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
            catch(Exception ex)
            {
                MsgBox.Show("Comuniquese con educa " +ex.Message, "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
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



        private void Window_Initialized(object sender, EventArgs e)
        {
            

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadImage();
            comboCity();
        }

        private void Modifbtn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    int id = int.Parse(txtid.Text);
            //    if (pathImagePortada != pathup)
            //    {

            //    }
            //    teacher = new Model.Teacher(id, txtnameMod.Text, txtlastnameMod.Text, txtsecondlastnameMod.Text, txtAddressMod.Text, txtPhoneMod.Text, txtemailMod.Text, ubicationPoint.Latitude, ubicationPoint.Longitude, idTown, pathup, pathImagePortada);
            //    teacherimpl = new TeacherImpl();
            //    int res = teacherimpl.Update(teacher);
            //    if (res > 0)
            //    {
            //        MessageBox.Show("Teacher Modifed successfully!!!");
            //        LoadDataGrid();
            //        ocultar();

            //    }
            //    else MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void Delbtn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    int id = int.Parse(txtid.Text);
            //    teacher = new Model.Teacher(id, txtnameDel.Text, txtlastnameDel.Text);
            //    teacherimpl = new TeacherImpl();
            //    teacherimpl.Delete(teacher);

            //    LoadDataGrid();
            //    ocultar();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Something happened \nCommunicate with the Suport department \neducateam.suport@gmail.com");
            //}
        }
        #endregion

        #region image
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

        #endregion;

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
                MessageBox.Show("Marked location");

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
            idTown = int.Parse(comboMuni.SelectedValue.ToString());
            MsgBox.Show("Seleccionado " + comboMuni.Text, "Atencion", MsgBox.Buttons.OK);
        }

        #endregion
    }
}
