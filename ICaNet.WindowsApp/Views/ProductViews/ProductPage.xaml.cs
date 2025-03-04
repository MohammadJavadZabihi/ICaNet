using System.Configuration;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ApiRequest.Net.CallApi;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNer.Shared.DTOs.Product;
using ICaNet.ApplicationCore.Entities.Products;
using ICaNet.WindowsApp.Views.CustomeLoading;
using ICaNet.WindowsApp.Views.CustomeMessageBox;
using ICaNet.WindowsApp.WpfService;

namespace ICaNet.WindowsApp.Views.ProductViews
{
    public partial class ProductPage : Page
    {
        private LoadFromApiService _loadFromApiService;
        private int pageSize = 10;
        private int itemSkip = 0;
        private string _apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string _apVersion = ConfigurationManager.AppSettings["ApiVersion"];

        public ProductPage()
        {
            InitializeComponent();
            _loadFromApiService = new LoadFromApiService();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (SaveProductClientDTO.Producs != null)
            {
                productDataGrid.ItemsSource = SaveProductClientDTO.Producs;
                var scrollViewer = GetScrollViewer(productDataGrid);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                }
            }
            else
            {
                CustomeLoadingWindow customeLoadingWindow = new CustomeLoadingWindow();
                customeLoadingWindow.Show();

                this.IsEnabled = false;

                await _loadFromApiService.LoadProducts(itemSkip, pageSize, txtSearcher.Text, append: false, productDataGrid);

                customeLoadingWindow.Close();

                this.IsEnabled = true;
                var scrollViewer = GetScrollViewer(productDataGrid);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                }
            }
        }

        private ScrollViewer GetScrollViewer(DependencyObject dep)
        {
            if (dep is ScrollViewer) return dep as ScrollViewer;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dep); i++)
            {
                var child = VisualTreeHelper.GetChild(dep, i);
                var result = GetScrollViewer(child);
                if (result != null)
                    return result;
            }
            return null;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)
                {
                    btnLoadMore.Visibility = Visibility.Visible;
                }
                else
                {
                    btnLoadMore.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void btnLoadMore_Click(object sender, RoutedEventArgs e)
        {
            itemSkip += 10;
            await _loadFromApiService.LoadProducts(itemSkip, pageSize, txtSearcher.Text, append: true, productDataGrid);
            btnLoadMore.Visibility = Visibility.Collapsed;
        }

        private async void txtSearcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            itemSkip = 0;
            await _loadFromApiService.LoadProducts(itemSkip, pageSize, txtSearcher.Text, append: false, productDataGrid);
        }

        private async void btnEdite_Click(object sender, RoutedEventArgs e)
        {
            if(productDataGrid.SelectedItem is GetProductResponse product)
            {
                AddNewProductWindw addNewProductWindw = new AddNewProductWindw(true, product.Name, product.Code, product.Statuce, product.UnitOfMeasurement,
                    product.Price, product.Count, product.Supplier, product.Id);

                addNewProductWindw.ShowDialog();

                if(addNewProductWindw.DialogResult == true)
                {
                    await _loadFromApiService.LoadProducts(itemSkip, pageSize, txtSearcher.Text, append: false, productDataGrid);
                }
            }
        }

        private async void btnDelet_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox customMessageBox = new CustomMessageBox("پیام", "آیا از حذف کالا مورد نظر مطمئن هستید؟", "باشه", "خیر", true);
            customMessageBox.ShowDialog();

            if (customMessageBox.DialogResult == true)
            {
                this.IsEnabled = false;

                CallApi callApi = new CallApi();

                try
                {
                    if (productDataGrid.SelectedItem is GetProductResponse product)
                    {
                        var data = new
                        {
                            ProduName = product.Name,
                            ProductCode = product.Code,
                        };

                        CustomeLoadingWindow customeLoadingWindow = new CustomeLoadingWindow();
                        customeLoadingWindow.Show();

                        var response = await callApi
                            .SendDeletRequest<DeleteProductRespone>($"{_apiUrl}/api/v{_apVersion}/Product/Delete", data, TokenManager.Token);

                        customeLoadingWindow.Close();

                        this.IsEnabled = true;

                        if (response.IsSuccess && response.Data.Result)
                        {
                            CustomMessageBox successMessageBox = new CustomMessageBox("پیام", $"{response.Data.Messgae}", "باشه", "", false);
                            successMessageBox.ShowDialog();

                            await _loadFromApiService.LoadProducts(itemSkip, pageSize, txtSearcher.Text, append: false, productDataGrid);
                        }
                        else
                        {
                            CustomMessageBox errorMessageBox = new CustomMessageBox("خطا", $"{response.Data.Messgae}", "باشه", "", false);
                            errorMessageBox.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    CustomMessageBox exeptionMessageBox = new CustomMessageBox("پیام", $"{ex.Message}", "باشه", "", false);
                    exeptionMessageBox.ShowDialog();
                }
            }
        }

        private async void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddNewProductWindw addNewProductWindw = new AddNewProductWindw();
            addNewProductWindw.ShowDialog();

            if(addNewProductWindw.DialogResult == true)
            {
                await _loadFromApiService.LoadProducts(itemSkip, pageSize, txtSearcher.Text, append: false, productDataGrid);
            }
        }
    }
}
