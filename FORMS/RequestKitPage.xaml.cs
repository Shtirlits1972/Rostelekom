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
    public partial class RequestKitPage : Page
    {
        List<RequestKit> RequestKitList = new List<RequestKit>();
        public RequestKitPage()
        {
            InitializeComponent();
            colEmployeesId.ItemsSource = EmployeesCrud.GetAll();
            colStatusRequestId.ItemsSource = StatusRequestCrud.GetAll();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RequestKitEdit edit = new RequestKitEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            RequestKit model = edit.model;

            if (model != null && model.Id > 0)
            {
                RequestKitList.Add(model);

                mainGrid.ItemsSource = RequestKitList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                RequestKitEdit edit = new RequestKitEdit();
                edit.IsEdit = true;
                RequestKit model = (RequestKit)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < RequestKitList.Count; i++)
                {
                    RequestKit item = RequestKitList[i];

                    if (model.Id == item.Id)
                    {
                        RequestKitList[i] = model;
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
                    RequestKit model = (RequestKit)mainGrid.SelectedItem;
                    RequestKitCrud.Del(model.Id);

                    RequestKitList.Remove(model);

                    mainGrid.ItemsSource = RequestKitList;
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
            RequestKitList = RequestKitCrud.GetAll();
            mainGrid.ItemsSource = RequestKitList;
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
                RequestKit model = (RequestKit)e.EditedItem;

                RequestKitCrud.Edit(model);

                for (int i = 0; i < RequestKitList.Count; i++)
                {
                    if (RequestKitList[i].Id == model.Id)
                    {
                        RequestKitList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = RequestKitList;
                mainGrid.Rebind();
            }
        }
    }
}
