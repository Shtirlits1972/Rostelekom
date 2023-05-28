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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rostelekom.FORMS
{
    public partial class EquipmentPage : Page
    {
        List<Equipment> EquipmentList = new List<Equipment>();
        public EquipmentPage()
        {
            InitializeComponent();
            colEquipmentTypeId.ItemsSource = EquipmentTypesCrud.GetAll();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EquipmentEdit edit = new EquipmentEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Equipment model = edit.model;

            if (model != null && model.Id > 0)
            {
                EquipmentList.Add(model);

                mainGrid.ItemsSource = EquipmentList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                EquipmentEdit edit = new EquipmentEdit();
                edit.IsEdit = true;
                Equipment model = (Equipment)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < EquipmentList.Count; i++)
                {
                    Equipment item = EquipmentList[i];

                    if (model.Id == item.Id)
                    {
                        EquipmentList[i] = model;
                        break;
                    }
                }
                mainGrid.Rebind();

            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                if (System.Windows.MessageBox.Show("Хотите удалить?", "Внимание", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
                {
                    Equipment model = (Equipment)mainGrid.SelectedItem;
                    EquipmentCrud.Del(model.Id);

                    EquipmentList.Remove(model);

                    mainGrid.ItemsSource = EquipmentList;
                    mainGrid.Rebind();
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Выберите строку!");
            }
        }
        private void btnSel_Click(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void GetAll()
        {
            EquipmentList = EquipmentCrud.GetAll();
            mainGrid.ItemsSource = EquipmentList;
            mainGrid.Rebind();
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.FilterDescriptors.SuspendNotifications();
            foreach (Telerik.Windows.Controls.GridViewColumn column in mainGrid.Columns)
            {
                column.ClearFilters();
            }
            mainGrid.FilterDescriptors.ResumeNotifications();
        }
        private void mainGrid_RowEditEnded(object sender, Telerik.Windows.Controls.GridViewRowEditEndedEventArgs e)
        {
            if (e.EditOperationType == Telerik.Windows.Controls.GridView.GridViewEditOperationType.Edit)
            {
                Equipment model = (Equipment)e.EditedItem;

                EquipmentCrud.Edit(model);

                for (int i = 0; i < EquipmentList.Count; i++)
                {
                    if (EquipmentList[i].Id == model.Id)
                    {
                        EquipmentList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = EquipmentList;
                mainGrid.Rebind();
            }
        }
    }
}
