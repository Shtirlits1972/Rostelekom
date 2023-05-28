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
    public partial class RequestKitEdit : Window
    {
        public bool IsEdit = false;
        public RequestKit model = new RequestKit();
        public RequestKitEdit()
        {
            InitializeComponent();

            comboEmployees.ItemsSource = EmployeesCrud.GetAll();
            comboEmployees.SelectedIndex = 0;

            comboStatusRequest.ItemsSource = StatusRequestCrud.GetAll();
            comboStatusRequest.SelectedIndex = 0;

            picCompletionDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            picCompletionDate.DisplayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            picRequestDate.SelectedDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            picRequestDate.DisplayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            model.EmployeesId = ((Employees)comboEmployees.SelectedItem).Id;
            model.EmployerName = ((Employees)comboEmployees.SelectedItem).EmployerName;

            model.StatusRequestId = ((StatusRequest)comboStatusRequest.SelectedItem).Id;
            model.StatusRequestName = ((StatusRequest)comboStatusRequest.SelectedItem).StatusRequestName;

            model.RequestDate = (DateTime)picRequestDate.SelectedDate;
            model.CompletionDate = (DateTime)picCompletionDate.SelectedDate;


            if (IsEdit)
            {
                RequestKitCrud.Edit(model);
            }
            else
            {
                model = RequestKitCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                picRequestDate.SelectedDate =  model.RequestDate;
                picRequestDate.DisplayDate = model.RequestDate;

                picCompletionDate.SelectedDate = model.CompletionDate;
                picCompletionDate.DisplayDate = model.CompletionDate;

                #region Employees
                for (int i = 0; i < comboEmployees.Items.Count; i++)
                {
                    Employees tmp = (Employees)comboEmployees.Items[i];
                    if (tmp.Id == model.EmployeesId)
                    {
                        comboEmployees.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                #region comboStatusRequest
                for (int i = 0; i < comboStatusRequest.Items.Count; i++)
                {
                    StatusRequest tmp = (StatusRequest)comboStatusRequest.Items[i];
                    if (tmp.Id == model.StatusRequestId)
                    {
                        comboStatusRequest.SelectedIndex = i;
                        break;
                    }
                }
                #endregion
            }
        }
    }
}
