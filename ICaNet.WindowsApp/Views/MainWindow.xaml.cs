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

namespace ICaNet.WindowsApp.Views
{
    public partial class MainWindow : Window
    {
        private string? apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string? apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var apiService = new CallApi();

            var response = await apiService.SendPostRequest<UserGetDashboardResponse>($"{apiUrl}/api/v{apiVersion}/user/GetDashboard", jwt: TokenManager.Token);
        }
    }
}
