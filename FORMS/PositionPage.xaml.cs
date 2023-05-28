﻿using Rostelekom.Crud;
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

    public partial class PositionPage : Page
    {
        List<Position> PositionList = new List<Position>();
        public PositionPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetAll();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PositionEdit edit = new PositionEdit();
            edit.IsEdit = false;

            edit.ShowDialog();

            Position model = edit.model;

            if (model != null && model.Id > 0)
            {
                PositionList.Add(model);

                mainGrid.ItemsSource = PositionList;
                mainGrid.Rebind();
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (mainGrid.SelectedItem != null)
            {
                PositionEdit edit = new PositionEdit();
                edit.IsEdit = true;
                Position model = (Position)mainGrid.SelectedItem;

                edit.model = model;
                edit.ShowDialog();

                model = edit.model;

                for (int i = 0; i < PositionList.Count; i++)
                {
                    Position item = PositionList[i];

                    if (model.Id == item.Id)
                    {
                        PositionList[i] = model;
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
                    Position model = (Position)mainGrid.SelectedItem;
                    PositionCrud.Del(model.Id);

                    PositionList.Remove(model);

                    mainGrid.ItemsSource = PositionList;
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
            PositionList = PositionCrud.GetAll();
            mainGrid.ItemsSource = PositionList;
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
                Position model = (Position)e.EditedItem;

                PositionCrud.Edit(model);

                for (int i = 0; i < PositionList.Count; i++)
                {
                    if (PositionList[i].Id == model.Id)
                    {
                        PositionList[i] = model;
                        break;
                    }
                }

                mainGrid.ItemsSource = PositionList;
                mainGrid.Rebind();
            }
        }

    }
}
