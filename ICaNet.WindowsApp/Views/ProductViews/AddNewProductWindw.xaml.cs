using ApiRequest.Net.CallApi;
using ApiRequest.Net.Servies.Models;
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

        private bool _isEdite;
        private string _productname;
        private string _productCode;
        private string _statuce;
        private string _unitOfMeasurement;
        private string _suppLier;
        private double _price;
        private double _count;
        private int _id;
        public AddNewProductWindw(bool isEdite = false, 
            string productname = "", 
            string productCode = "", 
            string statuce ="",
            string unitOfMeasurement = "",
            double price = 0,
            double count = 0,
            string suppLier = "",
            int id = 0)
        {
            InitializeComponent();
            _productname = productname;
            _productCode = productCode;
            _statuce = statuce;
            _unitOfMeasurement = unitOfMeasurement;
            _price = price;
            _count = count;
            _isEdite = isEdite;
            _suppLier = suppLier;
            _id = id;
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
            try
            {
                if (ValidateInputs() == false)
                {
                    ShowMessage("خطا", "لطفا اطلاعات مورد نیاز را پر نمایید");
                    return;
                }

                await SaveProduct();
            }
            catch (Exception)
            {
                ShowMessage("خطا", "خطای ناشناخته");
            }
        }

        private bool ValidateInputs()
        {
            return !string.IsNullOrEmpty(txtCode.Text) &&
                   !string.IsNullOrEmpty(txtName.Text) &&
                   !string.IsNullOrEmpty(txtPrice.Text) &&
                   !string.IsNullOrEmpty(cmbStatus.Text) &&
                   !string.IsNullOrEmpty(cmbSupplier.Text) &&
                   !string.IsNullOrEmpty(cmbUnitOfMeasurement.Text);
        }

        private void ShowMessage(string title, string message)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(title, message, "باشه", "", false);
            customMessageBox.ShowDialog();
        }

        private async Task SaveProduct()
        {
            CustomeLoadingWindow customeLoadingWindow = new CustomeLoadingWindow();
            customeLoadingWindow.Show();
            this.IsEnabled = false;

            try
            {
                var productData = CreateProductData();
                var response = await SendApiRequest(productData);
                ProcessResponse(response);
            }
            finally
            {
                customeLoadingWindow.Close();
                this.IsEnabled = true;
            }
        }

        private object CreateProductData()
        {
            if (_isEdite)
            {
                return new
                {
                    Id = _id,
                    Name = txtName.Text,
                    Price = Convert.ToDouble(txtPrice.Text),
                    Count = Convert.ToInt32(txtCount.Text),
                    UnitOfMeasurementName = cmbUnitOfMeasurement.Text,
                    Code = txtCode.Text,
                    SupplierName = cmbSupplier.Text,
                    Statuce = cmbStatus.Text,
                };
            }
            else
            {
                return new
                {
                    Name = txtName.Text,
                    Price = Convert.ToDouble(txtPrice.Text),
                    Count = Convert.ToInt32(txtCount.Text),
                    UnitOfMeasurementName = cmbUnitOfMeasurement.Text,
                    Code = txtCode.Text,
                    SupplierName = cmbSupplier.Text,
                    Statuce = cmbStatus.Text,
                };
            }
        }

        private async Task<dynamic> SendApiRequest(object data)
        {
            CallApi callApi = new CallApi();

            if (!_isEdite)
            {
                return await callApi.SendPostRequest<AddProductResponse>(
                    $"{_apiUrl}/api/v{_apiVersion}/Product/Add",
                    data,
                    TokenManager.Token);
            }
            else
            {
                return await callApi.SendPutRequest<EditeProductResponse>(
                    $"{_apiUrl}/api/v{_apiVersion}/Product/Update",
                    data,
                    TokenManager.Token);
            }
        }

        private void ProcessResponse(dynamic response)
        {
            if (response.IsSuccess && response.Data.Result)
            {
                ShowMessage("پیام", response.Data.Message);
                this.DialogResult = true;
                this.Close();
            }
            else if (response.Data != null)
            {
                ShowMessage("خطا", response.Data.Message);
                HandleFailedResponse();
            }
            else
            {
                ShowMessage("خطا", response.Message);
                HandleFailedResponse();
            }
        }

        private void HandleFailedResponse()
        {
            if (!_isEdite)
            {
                ClearInputs();
            }
            else
            {
                this.DialogResult = false;
                this.Close();
            }
        }

        private void ClearInputs()
        {
            txtCode.Text = "";
            txtPrice.Text = "";
            txtCount.Text = "";
            txtName.Text = "";
            cmbStatus.Text = "";
            cmbUnitOfMeasurement.Text = "";
            cmbSupplier.Text = "";
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(_isEdite)
            {
                txtCode.Text = _productCode;
                txtPrice.Text = _price.ToString();
                txtCount.Text = _count.ToString();
                txtName.Text = _productname;
                cmbStatus.Text = _statuce;
                cmbUnitOfMeasurement.Text = _unitOfMeasurement;
                cmbSupplier.Text = _suppLier;
            }
        }
    }
}
