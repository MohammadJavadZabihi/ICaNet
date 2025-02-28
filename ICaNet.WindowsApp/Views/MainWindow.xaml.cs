using ApiRequest.Net.CallApi;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNer.Shared.DTOs.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
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
using ICaNet.WindowsApp.Views.DashboardViews;

namespace ICaNet.WindowsApp.Views
{
    public partial class MainWindow : Window
    {
        private string? apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string? apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
        private Button _selectedButton;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.WindowState != WindowState.Maximized)
                    this.WindowState = WindowState.Maximized;
                else
                    this.WindowState = WindowState.Normal;
            }
            else
            {
                this.DragMove();
            }
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush(Colors.Transparent);
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            _selectedButton = sender as Button;

            if (_selectedButton != null)
            {
                _selectedButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#737373"));
                _selectedButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f1f1f1"));
            }

            Dashboard dashboard = new Dashboard();
            fContainer.Navigate(dashboard);
        }
    }
}
