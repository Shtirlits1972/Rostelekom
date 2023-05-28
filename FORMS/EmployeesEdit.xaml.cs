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
    public partial class EmployeesEdit : Window
    {
        public bool IsEdit = false;
        public Employees model = new Employees();
        public EmployeesEdit()
        {
            InitializeComponent();
            comboPosition.ItemsSource = PositionCrud.GetAll();
            comboPosition.SelectedIndex = 0;

            comboRoleEmployer.ItemsSource = Ut.UsersRoles;
            comboRoleEmployer.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            model = null;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
             model.EmployerName = txtEmployerName.Text;
            model.Telephone = txtTelephone.Text;
            model.EmployerName = txtEmployerName.Text;

            model.PositionId = ((Position)comboPosition.SelectedItem).Id;
            model.PositionName = ((Position)comboPosition.SelectedItem).PositionName;

            model.RoleEmployer = comboRoleEmployer.SelectedItem.ToString();


            if (IsEdit)
            {
                EmployeesCrud.Edit(model);
            }
            else
            {
                model = EmployeesCrud.Add(model);
            }
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsEdit)
            {
                txtEmployerName.Text = model.EmployerName;
                txtTelephone.Text = model.Telephone;
                txtEmployerName.Text = model.EmployerName;

                #region Position
                for (int i = 0; i < comboPosition.Items.Count; i++)
                {
                    Position tmp = (Position)comboPosition.Items[i];
                    if (tmp.Id == model.PositionId)
                    {
                        comboPosition.SelectedIndex = i;
                        break;
                    }
                }
                #endregion

                #region RoleEmployer
                for (int i = 0; i < comboRoleEmployer.Items.Count; i++)
                {
                    string tmp = (string)comboRoleEmployer.Items[i];
                    if (tmp == model.RoleEmployer)
                    {
                        comboRoleEmployer.SelectedIndex = i;
                        break;
                    }
                }
                #endregion
            }
        }
    }
}
