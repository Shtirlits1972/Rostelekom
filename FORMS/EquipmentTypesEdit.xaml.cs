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

namespace Rostelekom.FORMS
{
    public partial class EquipmentTypesEdit : Window
    {
        public bool IsEdit = false;
        public EquipmentTypes model = new EquipmentTypes();
        public EquipmentTypesEdit()
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
            model.EquipmentTypeName = txtName.Text;
            if (IsEdit)
            {
                EquipmentTypesCrud.Edit(model);
            }
            else
            {
                model = EquipmentTypesCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtName.Text = model.EquipmentTypeName;
            }
        }
    }
}
