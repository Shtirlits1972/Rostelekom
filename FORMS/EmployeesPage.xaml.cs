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
    public partial class EmployeesPage : Page
    {
        List<Employees> EmployeesList = new List<Employees>();
        public EmployeesPage()
        {
            InitializeComponent();
            mainGrid.ItemsSource = EmployeesCrud.GetAll();
            try
            {
                colPositionId.ItemsSource = PositionCrud.GetAll();
                colRoleEmployer.ItemsSource = Ut.UsersRoles;
            }
            catch { }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeesEdit edit = new EmployeesEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Employees model = edit.model;

            if (model != null && model.Id > 0)
            {
                EmployeesList.Add(model);

                mainGrid.ItemsSource = EmployeesList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                EmployeesEdit edit = new EmployeesEdit();
                edit.IsEdit = true;
                Employees model = (Employees)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < EmployeesList.Count; i++)
                {
                    Employees item = EmployeesList[i];

                    if (model.Id == item.Id)
                    {
                        EmployeesList[i] = model;
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
                    Employees model = (Employees)mainGrid.SelectedItem;
                    EmployeesCrud.Del(model.Id);

                    EmployeesList.Remove(model);

                    mainGrid.ItemsSource = EmployeesList;
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
            EmployeesList = EmployeesCrud.GetAll();
            mainGrid.ItemsSource = EmployeesList;
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
                Employees model = (Employees)e.EditedItem;

                EmployeesCrud.Edit(model);

                for (int i = 0; i < EmployeesList.Count; i++)
                {
                    if (EmployeesList[i].Id == model.Id)
                    {
                        EmployeesList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = EmployeesList;
                mainGrid.Rebind();
            }
        }
    }
}
