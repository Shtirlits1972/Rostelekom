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
    public partial class SuppliersPage : Page
    {
        List<Suppliers> SuppliersList = new List<Suppliers>();
        public SuppliersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SuppliersEdit edit = new SuppliersEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Suppliers model = edit.model;

            if (model != null && model.Id > 0)
            {
                SuppliersList.Add(model);

                mainGrid.ItemsSource = SuppliersList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                SuppliersEdit edit = new SuppliersEdit();
                edit.IsEdit = true;
                Suppliers model = (Suppliers)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < SuppliersList.Count; i++)
                {
                    Suppliers item = SuppliersList[i];

                    if (model.Id == item.Id)
                    {
                        SuppliersList[i] = model;
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
                    Suppliers model = (Suppliers)mainGrid.SelectedItem;
                    SuppliersCrud.Del(model.Id);

                    SuppliersList.Remove(model);

                    mainGrid.ItemsSource = SuppliersList;
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
            SuppliersList = SuppliersCrud.GetAll();
            mainGrid.ItemsSource = SuppliersList;
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
                Suppliers model = (Suppliers)e.EditedItem;

                SuppliersCrud.Edit(model);

                for (int i = 0; i < SuppliersList.Count; i++)
                {
                    if (SuppliersList[i].Id == model.Id)
                    {
                        SuppliersList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = SuppliersList;
                mainGrid.Rebind();
            }
        }
    }
}
