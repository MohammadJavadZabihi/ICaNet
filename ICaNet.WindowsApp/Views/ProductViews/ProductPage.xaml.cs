using ApiRequest.Net.CallApi;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNer.Shared.DTOs.Product;
using ICaNet.ApplicationCore.Entities.Products;
using ICaNet.WindowsApp.Views.CustomeLoading;
using ICaNet.WindowsApp.Views.CustomeMessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

namespace ICaNet.WindowsApp.Views.ProductViews
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private CallApi _callApi;
        private string? apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string? apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
        public ProductPage()
        {
            InitializeComponent();
            _callApi = new CallApi();
        }

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            CustomeLoadingWindow customeLoadingWindow = new CustomeLoadingWindow();
            customeLoadingWindow.Show();
            this.IsEnabled = false;


            var data = new
            {
                PageNumber = 1,
                Filter = txtSearcher.Text,
                PageSize = 10
            };

            var response = await _callApi
                .SendPostRequest<List<GetProductResponse>>($"{apiUrl}/api/v{apiVersion}/Product/GetAll", data, TokenManager.Token);

            customeLoadingWindow.Close();

            this.IsEnabled = true;

            if(response.IsSuccess)
            {
                var data2 = response.Data;

                productDataGrid.ItemsSource = data2;
            }
            else
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("خطا", $"{response.Message}", "باشه", "", false);
                customMessageBox.ShowDialog();
            }
        }

        private async void txtSearcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            CustomeLoadingWindow customeLoadingWindow = new CustomeLoadingWindow();
            customeLoadingWindow.Show();


            var data = new
            {
                PageNumber = 1,
                Filter = txtSearcher.Text,
                PageSize = 10
            };

            var response = await _callApi
                .SendPostRequest<List<GetProductResponse>>($"{apiUrl}/api/v{apiVersion}/Product/GetAll", data, TokenManager.Token);

            customeLoadingWindow.Close();

            if (response.IsSuccess)
            {
                var data2 = response.Data;

                productDataGrid.ItemsSource = data2;
            }
            else
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("خطا", $"{response.Message}", "باشه", "", false);
                customMessageBox.ShowDialog();
            }
        }
    }
}
