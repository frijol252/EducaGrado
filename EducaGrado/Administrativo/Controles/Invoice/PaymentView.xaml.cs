using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EducaGrado.Administrativo.Controles.Invoice
{
    /// <summary>
    /// Lógica de interacción para PaymentView.xaml
    /// </summary>
    public partial class PaymentView : Window
    {
        int idStudent;
        public PaymentView(int idStudent)
        {
            InitializeComponent();
            this.idStudent = idStudent;
            
        }
        Payer payer;
        PayerImpl payerImpl;
        int idpayer = 0;
        FeeImpl feeImpl;
        double totalfinal = 0;
        List<Fee> idFees = new List<Fee>();

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void LoadDataGrid()
        {
            try
            {
                feeImpl = new FeeImpl();
                dgvSub.ItemsSource = null;
                dgvSub.ItemsSource = feeImpl.SelectByStudent(idStudent).DefaultView;

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void dgvSub_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (dgvSub.Items.Count > 0 && dgvSub.SelectedItem != null)
            {
                try
                {

                    DataRowView dataRow = (DataRowView)dgvSub.SelectedItem;
                    string cellValue = dataRow.Row.ItemArray[1].ToString();
                    idFees.Clear();
                    totalfinal = 0;
                    int count = 0;
                    if (cellValue == "-")
                    {
                        foreach (DataRowView row in dgvSub.ItemsSource)
                        {
                            if (count == dgvSub.Items.IndexOf(dgvSub.CurrentItem))
                            {
                                row[1] = "Selecionado";
                            }
                            if(row[1].ToString() == "Selecionado")
                            {
                                idFees.Add( new Fee(int.Parse(row[0].ToString()), Convert.ToDouble(row[2].ToString()), 1));
                                totalfinal += Convert.ToDouble(row[2].ToString());
                            }
                            count++;
                        }
                        
                    }
                    else if (cellValue == "Selecionado")
                    {
                        foreach (DataRowView row in dgvSub.ItemsSource)
                        {
                            if (count == dgvSub.Items.IndexOf(dgvSub.CurrentItem))
                            {
                                row[1] = "-";
                            }
                            if(row[1].ToString() == "Selecionado")
                            {
                                idFees.Add(new Fee(int.Parse(row[0].ToString()), Convert.ToDouble(row[2].ToString()),1));
                                totalfinal += Convert.ToDouble(row[2].ToString());
                            }
                            count++;
                        }
                    }
                    
                    lblamount.Content = totalfinal.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void txtnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                payerImpl = new PayerImpl();
                DataTable revision = new DataTable();
                revision = payerImpl.Select(txtnit.Text);
                if (revision.Rows.Count!=0)
                {
                    foreach (DataRow row in revision.Rows)
                    {
                        lblbuss.Content = row[1].ToString();
                        idpayer = int.Parse(row[0].ToString());
                    }
                    
                }
                else
                {
                    PayerViewAdd payerViewAdd = new PayerViewAdd(txtnit.Text);
                    payerViewAdd.Show();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataGrid();
        }

        private void Addsubject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (validar())
                {
                    if (llenar())
                    {
                        
                    }
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show("Comuniquese con el equipo de educa error"+ex.Message, "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
            }
        }
        private bool llenar()
        {
            double amount = Convert.ToDouble(txtamount.Text.Replace(",", "."));
            foreach (Fee fee in idFees)
            {
                if (amount > 0)
                {
                    if (amount >= fee.Balance)
                    {
                        amount = amount - fee.Balance;
                    }
                    else
                    {
                        fee.Balance = amount;
                    }
                }
                else
                {
                    MsgBox.Show("El pago no cumple para todas las cuotas", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                    return false;
                }
            }
            return true;
        }
        public bool validar()
        {
            bool validacion = false;
            double monto = 0;
            if(!string.IsNullOrEmpty(txtamount.Text.Replace(",", ".")))
            {
                monto= Convert.ToDouble(txtamount.Text.Replace(",", "."));
            }

            if (idpayer != 0) 
            {
                if (idFees.Count>0)
                {
                    if (monto==0)
                    {
                        MsgBox.Show("El pago no puede ser 0", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                        
                    }
                    else
                    {
                        if (monto <= totalfinal)
                        {
                            validacion = true;
                        }
                        else
                        {
                            MsgBox.Show("El pago no puede ser mayor al total a pagar", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                        }
                    }
                }
                else
                {
                    MsgBox.Show("Debe seleccionar alguna cuota", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                }
            }
            else
            {
                MsgBox.Show("Inserte un contribuidor/NIT", "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
            }


            return validacion;
        }

        private void txtamount_TextChanged(object sender, TextChangedEventArgs e)
        {
            idFees.Clear();
            foreach (DataRowView row in dgvSub.ItemsSource)
            {
                if (row[1].ToString() == "Selecionado")
                {
                    idFees.Add(new Fee(int.Parse(row[0].ToString()), Convert.ToDouble(row[2].ToString()), 1));
                    totalfinal += Convert.ToDouble(row[2].ToString());
                }
            }
        }
    }
}
