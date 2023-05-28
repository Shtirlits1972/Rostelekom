using Rostelekom.FORMS;
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
using Telerik.Windows.Controls;

namespace Rostelekom
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LocalizationManager.Manager = new CustomLocalizationManager();
            InitializeComponent();
        }

        private void PositionPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/PositionPage.xaml", UriKind.Relative));
            Title = "Должность";
        }

        private void EquipmentTypesPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/EquipmentTypesPage.xaml", UriKind.Relative));
            Title = "Типы оборудования";
        }

        private void StatusRequestPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/StatusRequestPage.xaml", UriKind.Relative));
            Title = "Статус заявки";
        }

        private void SuppliersPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/SuppliersPage.xaml", UriKind.Relative));
            Title = "Поставщики";
        }

        private void MenuEmployees_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/EmployeesPage.xaml", UriKind.Relative));
            Title = "Сотрудники";
        }

        private void MenuRequestKitPage_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/RequestKitPage.xaml", UriKind.Relative));
            Title = "Заявки на комплектацию";
        }

        private void EquipmentPageBtn_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/EquipmentPage.xaml", UriKind.Relative));
            Title = "Оборудование";
        }

        private void MenuInventoryPage_Click(object sender, RoutedEventArgs e)
        {
            frmContent.NavigationService.Navigate(new Uri("FORMS/InventoryPage.xaml", UriKind.Relative));
            Title = "Инвентаризация";
        }
    }
}
