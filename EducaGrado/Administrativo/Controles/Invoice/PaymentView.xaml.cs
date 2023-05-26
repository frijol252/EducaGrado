using EducaGrado.xDialog;
using Implementation;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        string studentName;
        public PaymentView(int idStudent,string name)
        {
            InitializeComponent();
            this.idStudent = idStudent;
            this.studentName = name;
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
                        setDosage();
                        Model.Invoice invoice = generarinvoice();
                        PaymentImpl payment = new PaymentImpl();
                        payment.InsertTransact(idFees, generarinvoice());
                        MsgBox.Show("Se registro el pago", "", MsgBox.Buttons.OK, MsgBox.Icon.Info);
                        int a = DBImplementation.GetIdentityFromTable("Invoice")- DBImplementation.GetIncementFromTable("Invoice");
                        
                        RevisionInvoice revision = new RevisionInvoice(a);
                        revision.Show();
                        this.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MsgBox.Show("Comuniquese con el equipo de educa error"+ex.Message, "Atencion", MsgBox.Buttons.OK, MsgBox.Icon.Error);
            }
        }
       /* private InvoicePayed datos()
        {
            
        }*/
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
                        fee.Status = 0;
                    }
                    else
                    {
                        fee.Balance = amount;
                        fee.Status = 1;
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
            if(!string.IsNullOrEmpty(txtamount.Text))
            {
                string revisionamount = txtamount.Text.Replace(",", ".");
                
                monto = Double.Parse(revisionamount, CultureInfo.InvariantCulture);
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
        Dosage dosage;
        DosageImpl dosageImpl;
        
        public Model.Invoice generarinvoice()
        {
            string revisionamount = txtamount.Text.Replace(",", ".");
            Model.Invoice invoice = new Model.Invoice();
            invoice.Amount = Double.Parse(revisionamount, CultureInfo.InvariantCulture);
            invoice.NroInvoice = dosage.FinalNumber + 1;
            invoice.ControlCode = retCodeControl();
            invoice.IdDosage = dosage.DosageId;
            invoice.IdPayer = idpayer;
            invoice.Literal = LiteralClass.NumeroALetras(Decimal.Parse(revisionamount.Replace(".",",")));
            
            return invoice;
        }
        public void setDosage()
        {
            dosage = new Dosage();
            dosageImpl = new DosageImpl();
            dosage = dosageImpl.GET();
        }
        public string retCodeControl()
        {
            
            return ControlCode.generateControlCode(dosage.NroAutorization,
                            dosage.FinalNumber.ToString(),
                            idpayer.ToString(),
                            "" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day,
                            totalfinal.ToString(),
                            dosage.DosageKey);
        }

        private void txtnit_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
