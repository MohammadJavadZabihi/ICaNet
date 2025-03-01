using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ICaNer.Shared.DTOs.Product;
using ICaNet.WindowsApp.Views.CustomeLoading;
using ICaNet.WindowsApp.WpfService;

namespace ICaNet.WindowsApp.Views.ProductViews
{
    public partial class ProductPage : Page
    {
        private LoadFromApiService _loadFromApiService;
        private int pageSize = 10;
        private int itemSkip = 0;

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

        private void btnEdite_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
