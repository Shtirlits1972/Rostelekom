using Rostelekom.Crud;
using Rostelekom.Models;
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
using System.Xml.Linq;

namespace Rostelekom.FORMS
{
    public partial class SuppliersEdit : Window
    {
        public bool IsEdit = false;
        public Suppliers model = new Suppliers();
        public SuppliersEdit()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.SupplierName = txtSupplierName.Text;
            model.ContactPerson = txtContactPerson.Text;
            model.ContactNumber = txtContactNumber.Text;
            model.Terms = txtTerms.Text;

            if (IsEdit)
            {
                SuppliersCrud.Edit(model);
            }
            else
            {
                model = SuppliersCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtSupplierName.Text = model.SupplierName;
                txtContactPerson.Text = model.ContactPerson;
                txtContactNumber.Text = model.ContactNumber;
                txtTerms.Text = model.Terms;
            }
        }
    }
}
