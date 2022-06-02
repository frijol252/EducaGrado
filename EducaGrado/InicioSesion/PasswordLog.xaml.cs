using Implementation;
using Model;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace EducaGrado.InicioSesion
{
    /// <summary>
    /// Lógica de interacción para PasswordLog.xaml
    /// </summary>
    public partial class PasswordLog : Window
    {
        public PasswordLog(int userId)
        {
            InitializeComponent();

            this.userId = userId;
        }
        int userId;
        UserImpl implUser;
        UserAccount userAccount = new UserAccount();
        private void Window_Initialized(object sender, EventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string pass = txtpass.Password;
                string passconf = txtconfirm.Password;
                userAccount = new UserAccount();
                userAccount.UserID = userId;

                if (!string.IsNullOrEmpty(pass) || !string.IsNullOrEmpty(passconf))
                {
                    if (pass == passconf)
                    {

                        encriptadoAES(pass);
                        userAccount.Password = encriptado;
                        userAccount.Key = Key;
                        userAccount.VI = IV;
                        userAccount.RevisionPass = 1;
                        implUser = new UserImpl();
                        int res = implUser.Update(userAccount);
                        if (res > 0)
                        {
                            MessageBox.Show("Contraseña Cambiada");
                            Index log = new Index();
                            log.Show();
                            this.Close();
                        }

                    }
                    else MessageBox.Show("Las contraseñas no concuerdan");
                }
                else MessageBox.Show("Todos los campos tienen que estar llenados");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Algo ocurrio comuniquese con el soporte");
            }
        }

        #region Encriptar

        static byte[] Key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32}; //Llave de encriptación
        static byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }; // Vector de inicialización
        static byte[] encriptado;
        static AesManaged aes = new AesManaged();
        static string encriptadoAES(string cadena)
        {
            string salida = "";
            // Generación de la llave de encriptación y del vector de inicialización
            Key = aes.Key;
            IV = aes.IV;
            try
            {
                using (aes)
                {
                    encriptado = Encriptar(cadena);
                    salida = $"{Encoding.UTF8.GetString(encriptado)}";
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Excepcion: " + exp.Message);
                Console.ReadKey();
            }
            return salida;
        }

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
    }
}
