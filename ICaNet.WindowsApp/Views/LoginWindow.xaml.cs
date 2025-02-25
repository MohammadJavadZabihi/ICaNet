using ApiRequest.Net.CallApi;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNet.WindowsApp.Views;
using ICaNet.WindowsApp.Views.CustomeMessageBox;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ICaNet.WindowsApp
{
    public partial class MainWindow : Window
    {
        private string? apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string? apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1.75));
            this.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnExite_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                CustomMessageBox customMessageBox = new CustomMessageBox("خطای کاربر","لطفا فیلدهای مورد نیاز را پر نمایید!", "باشه", "", false);
                customMessageBox.ShowDialog();
            }
            else
            {
                var apiService = new CallApi();

                var data = new
                {
                    UserName = txtUserName.Text,
                    Password = txtPassword.Password,
                };

                var response = await apiService.SendPostRequest<AuthenticateResponse>($"{apiUrl}/api/v{apiVersion}/authenticate/generateToken", data);

                if(!response.Data.Result)
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("خطای کاربر", "لطفا در ارسال اطلاعات دقت فرمایید", "باشه", "", false);
                    customMessageBox.ShowDialog();
                }
                else
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("خوش آمد گویی", $"خوش آمدید {response.Data.Username}", "باشه", "", false);
                    customMessageBox.ShowDialog();
                }
            }
        }
    }
}