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
    public partial class EquipmentEdit : Window
    {
        public bool IsEdit = false;
        public Equipment model = new Equipment();
        public EquipmentEdit()
        {
            InitializeComponent();

            comboEquipmentTypeId.ItemsSource = EquipmentTypesCrud.GetAll();
            comboEquipmentTypeId.SelectedIndex = 0;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.EquipmentName = txtEquipmentName.Text;
            model.EquipmentModel = txtEquipmentModel.Text;
            model.SerialNumber = txtSerialNumber.Text;
            model.Manufacturer = txtManufacturer.Text;

            model.EquipmentTypeId = ((EquipmentTypes)comboEquipmentTypeId.SelectedItem).Id;
            model.EquipmentTypeName = ((EquipmentTypes)comboEquipmentTypeId.SelectedItem).EquipmentTypeName;

            if (IsEdit)
            {
                EquipmentCrud.Edit(model);
            }
            else
            {
                model = EquipmentCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtEquipmentName.Text =  model.EquipmentName;
                txtEquipmentModel.Text =  model.EquipmentModel;
                txtSerialNumber.Text = model.SerialNumber;
                txtManufacturer.Text = model.Manufacturer;

                #region EquipmentTypes
                for (int i = 0; i < comboEquipmentTypeId.Items.Count; i++)
                {
                    EquipmentTypes tmp = (EquipmentTypes)comboEquipmentTypeId.Items[i];
                    if (tmp.Id == model.EquipmentTypeId)
                    {
                        comboEquipmentTypeId.SelectedIndex = i;
                        break;
                    }
                }
                #endregion
            }
        }
    }
}
