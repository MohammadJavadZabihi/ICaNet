using ApiRequest.Net.CallApi;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNer.Shared.DTOs.Product;
using ICaNet.WindowsApp.Views.CustomeMessageBox;
using System.Configuration;
using System.Windows.Controls;

namespace ICaNet.WindowsApp.WpfService
{
    public class LoadFromApiService
    {
        private CallApi _callApi;
        private string? apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string? apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
        public LoadFromApiService()
        {
            _callApi = new CallApi();
        }
        public async Task LoadProducts(int itemSkip,int pageSize, string filter, bool append, DataGrid productDataGrid)
        {
            var requestData = new
            {
                ItemSkip = itemSkip,
                Filter = filter,
                PageSize = pageSize
            };

            var response = await _callApi
                .SendPostRequest<List<GetProductResponse>>($"{apiUrl}/api/v{apiVersion}/Product/GetAll", requestData, TokenManager.Token);

            if (response.IsSuccess)
            {
                var data = response.Data;
                if (append && SaveProductClientDTO.Producs != null)
                {
                    SaveProductClientDTO.Producs.AddRange(data);
                    productDataGrid.ItemsSource = null;
                    productDataGrid.ItemsSource = SaveProductClientDTO.Producs;
                }
                else
                {
                    SaveProductClientDTO.Producs = data;
                    productDataGrid.ItemsSource = data;
                }
            }
            else
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("خطا", $"{response.Message}", "باشه", "", false);
                customMessageBox.ShowDialog();
            }
        }
    }
}
