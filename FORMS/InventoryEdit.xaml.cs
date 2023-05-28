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
    public partial class InventoryEdit : Window
    {
        public bool IsEdit = false;
        public Inventory model = new Inventory();
        public InventoryEdit()
        {
            InitializeComponent();
            comboEquipmentId.ItemsSource = EquipmentCrud.GetAll();
            comboEquipmentId.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)comboEquipmentId.SelectedItem;
            model.EquipmentId = equipment.Id;
            model.EquipmentName = equipment.EquipmentName;

            int intQuantity = 0;
            int.TryParse(txtQuantity.Text.Trim(), out intQuantity);
            model.Quantity = intQuantity;

            model.Location = txtLocation.Text;
            model.Availability = (bool)checkAvailability.IsChecked;

            if (IsEdit)
            {
                InventoryCrud.Edit(model);
            }
            else
            {
                model = InventoryCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                #region Equipment
                for (int i = 0; i < comboEquipmentId.Items.Count; i++)
                {
                    Equipment tmp = (Equipment)comboEquipmentId.Items[i];
                    if (tmp.Id == model.EquipmentId)
                    {
                        comboEquipmentId.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                txtQuantity.Text =  model.Quantity.ToString();

                txtLocation.Text = model.Location;
                checkAvailability.IsChecked = model.Availability;
            }
        }
    }
}
