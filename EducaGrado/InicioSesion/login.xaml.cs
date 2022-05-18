using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.IO;
using System.Security.Cryptography;
using System.Data;

namespace EducaGrado.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para Index.xaml
    /// </summary>
    public partial class Index : Window
    {
        UserImpl implUser;
        private bool accept = false;
        private bool revisara = false;
        UserAccount userAccount;

        static byte[] Key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32}; //Llave de encriptación
        static byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; // Vector de inicialización
        static byte[] encriptado;
        static byte state;
        static AesManaged aes = new AesManaged();


        public Index()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ForgotPassword fp = new ForgotPassword();
            fp.Show();
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string user = txtUsername.Text.ToLower();
                string password = txtPassword.Text;
                if (!string.IsNullOrEmpty(user))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        implUser = new UserImpl();
                        DataTable dt = implUser.GET(user);
                        if (dt.Rows.Count > 0)
                        {
                            encriptado = Encoding.Default.GetBytes(dt.Rows[0][2].ToString());
                            Key = Encoding.Default.GetBytes(dt.Rows[0][3].ToString());
                            IV = Encoding.Default.GetBytes(dt.Rows[0][4].ToString());
                            state = Convert.ToByte(dt.Rows[0][5].ToString());
                            if (state == 1)
                            {
                                if (Validar(password))
                                {
                                    Administrativo.Home.HomeAdmin HA = new Administrativo.Home.HomeAdmin();
                                    HA.Show();
                                    this.Close();
                                }
                                    
                                else MessageBox.Show("Algo ocurrio Comunicate con educa");
                            }
                            else MessageBox.Show("Su usuario esta bloqueado  comuniquese con su organizacion");
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña erroneos");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Llena todos los espacion");
                    }
                }
                else 
                {
                    MessageBox.Show("Llena todos los espacion");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio Comunicate con educa" + ex.Message);
            }
        }

        private bool Validar(string password)
        {
            return Encriptar(password).SequenceEqual(encriptado);
        }



        #region Encriptar
        static byte[] Encriptar(string texto)
        {
            byte[] encriptando;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encriptador = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encriptador, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(texto);
                        encriptando = ms.ToArray();
                    }
                }
            }
            return encriptando;
        }
        
        #endregion
        /*
         * string user = "educasa";
            string password = "educa2020";
            string ASD = "";
            encriptadoAES(password);
            userAccount = new UserAccount(user, encriptado, Key, IV, 1);
            implUser = new UserImpl();
            implUser.Insert(userAccount);
            MessageBox.Show(Key.Length.ToString());
            for (int ciclo = 0; ciclo < 32; ciclo++)
                ASD += (Key[ciclo] + "-");
            MessageBox.Show(ASD);

            implUser = new UserImpl();
            DataTable dt = implUser.GET();
            if (dt.Rows.Count > 0)
            {
                ASD += "\n";
                user = dt.Rows[0][1].ToString();
                encriptado = Encoding.Default.GetBytes(dt.Rows[0][2].ToString());
                Key = Encoding.Default.GetBytes(dt.Rows[0][3].ToString());
                IV = Encoding.Default.GetBytes(dt.Rows[0][4].ToString());
                for (int ciclo = 0; ciclo < 32; ciclo++)
                    ASD += (Key[ciclo] + "-");
                MessageBox.Show(Key.Length.ToString());
                MessageBox.Show(ASD);
                txtPassword.Text = Desencripta(encriptado);
            }
            else
            {

            }
         */
    }


}
