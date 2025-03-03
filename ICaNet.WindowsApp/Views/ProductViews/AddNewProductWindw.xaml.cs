using ApiRequest.Net.CallApi;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNer.Shared.DTOs.Product;
using ICaNet.WindowsApp.Views.CustomeLoading;
using ICaNet.WindowsApp.Views.CustomeMessageBox;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

namespace ICaNet.WindowsApp.Views.ProductViews
{
    public partial class AddNewProductWindw : Window
    {
        private string? _apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string? _apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
        public AddNewProductWindw()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(txtCode.Text) || !string.IsNullOrEmpty(txtCount.Text) || 
                !string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtPrice.Text))
            {
                CustomMessageBox messageBox =
                        new CustomMessageBox("هشدار", "محصول شما هنوز ثبت نشده است. آیا از بستن صفحه مطمئن هستید؟", "بله", "خیر", true);
                messageBox.ShowDialog();

                if(messageBox.DialogResult == true)
                { 
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CustomeLoadingWindow customeLoadingWindow = new CustomeLoadingWindow();
            customeLoadingWindow.Show();

            this.IsEnabled = false;

            CallApi callApi = new CallApi();

            var data = new
            {
                Name = txtName.Text,
                Price = Convert.ToDouble(txtPrice.Text),
                Count = Convert.ToInt32(txtCount.Text),
                UnitOfMeasurementName = cmbUnitOfMeasurement.Text,
                Code = txtCode.Text,
                SupplierName = cmbSupplier.Text,
                Statuce = cmbStatus.Text,
            };

            var response = await callApi.SendPostRequest<AddProductResponse>($"{_apiUrl}/api/v{_apiVersion}/Product/Add", data, TokenManager.Token);

            customeLoadingWindow.Close();

            this.IsEnabled = true;

            if (response.IsSuccess && response.Data.Result)
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("پیام", $"{response.Data.Messaeg}", "باشه", "", false);
                customMessageBox.ShowDialog();

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("پیام", $"{response.Message}", "باشه", "", false);
                customMessageBox.ShowDialog();

                this.DialogResult = false;
                this.Close();
            }
        }

        private void txtCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int result;
            e.Handled = !int.TryParse(e.Text, out result);
        }

        private void txtPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int result;
            e.Handled = !int.TryParse(e.Text, out result);
        }
    }
}
