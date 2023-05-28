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
    public partial class EquipmentTypesPage : Page
    {
        List<EquipmentTypes> EquipmentTypesList = new List<EquipmentTypes>();
        public EquipmentTypesPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EquipmentTypesEdit edit = new EquipmentTypesEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            EquipmentTypes model = edit.model;

            if (model != null && model.Id > 0)
            {
                EquipmentTypesList.Add(model);

                mainGrid.ItemsSource = EquipmentTypesList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                EquipmentTypesEdit edit = new EquipmentTypesEdit();
                edit.IsEdit = true;
                EquipmentTypes model = (EquipmentTypes)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < EquipmentTypesList.Count; i++)
                {
                    EquipmentTypes item = EquipmentTypesList[i];

                    if (model.Id == item.Id)
                    {
                        EquipmentTypesList[i] = model;
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
                    EquipmentTypes model = (EquipmentTypes)mainGrid.SelectedItem;
                    EquipmentTypesCrud.Del(model.Id);

                    EquipmentTypesList.Remove(model);

                    mainGrid.ItemsSource = EquipmentTypesList;
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
            EquipmentTypesList = EquipmentTypesCrud.GetAll();
            mainGrid.ItemsSource = EquipmentTypesList;
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
                EquipmentTypes model = (EquipmentTypes)e.EditedItem;

                EquipmentTypesCrud.Edit(model);

                for (int i = 0; i < EquipmentTypesList.Count; i++)
                {
                    if (EquipmentTypesList[i].Id == model.Id)
                    {
                        EquipmentTypesList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = EquipmentTypesList;
                mainGrid.Rebind();
            }
        }
    }
}
