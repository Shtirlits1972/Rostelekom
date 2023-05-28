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
    public partial class StatusRequestPage : Page
    {
        List<StatusRequest> StatusRequestList = new List<StatusRequest>();
        public StatusRequestPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StatusRequestEdit edit = new StatusRequestEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            StatusRequest model = edit.model;

            if (model != null && model.Id > 0)
            {
                StatusRequestList.Add(model);

                mainGrid.ItemsSource = StatusRequestList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                StatusRequestEdit edit = new StatusRequestEdit();
                edit.IsEdit = true;
                StatusRequest model = (StatusRequest)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < StatusRequestList.Count; i++)
                {
                    StatusRequest item = StatusRequestList[i];

                    if (model.Id == item.Id)
                    {
                        StatusRequestList[i] = model;
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
                    StatusRequest model = (StatusRequest)mainGrid.SelectedItem;
                    StatusRequestCrud.Del(model.Id);

                    StatusRequestList.Remove(model);

                    mainGrid.ItemsSource = StatusRequestList;
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
            StatusRequestList = StatusRequestCrud.GetAll();
            mainGrid.ItemsSource = StatusRequestList;
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
                StatusRequest model = (StatusRequest)e.EditedItem;

                StatusRequestCrud.Edit(model);

                for (int i = 0; i < StatusRequestList.Count; i++)
                {
                    if (StatusRequestList[i].Id == model.Id)
                    {
                        StatusRequestList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = StatusRequestList;
                mainGrid.Rebind();
            }
        }
    }
}
