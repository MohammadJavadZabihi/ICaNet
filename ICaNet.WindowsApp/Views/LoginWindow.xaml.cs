using ApiRequest.Net.CallApi;
using ICaNer.Shared.DTOs.Authenticate;
using ICaNet.WindowsApp.Views.CustomeLoading;
using ICaNet.WindowsApp.Views.CustomeMessageBox;
using System.Configuration;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ICaNet.WindowsApp.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string? apiUrl = ConfigurationManager.AppSettings["ApiURL"];
        private string? apiVersion = ConfigurationManager.AppSettings["ApiVersion"];
        public LoginWindow()
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
                CustomMessageBox nullFieldErrorMessage = new CustomMessageBox("خطای کاربر", "لطفا فیلدهای مورد نیاز را پر نمایید!", "باشه", "", false);
                nullFieldErrorMessage.ShowDialog();
            }
            else
            {
                var apiService = new CallApi();

                var data = new
                {
                    UserName = txtUserName.Text,
                    Password = txtPassword.Password,
                };

                this.IsEnabled = false;

                CustomeLoadingWindow customeLoadingWindow = new CustomeLoadingWindow();
                customeLoadingWindow.Show();

                var response = await apiService.SendPostRequest<AuthenticateResponse>($"{apiUrl}/api/v{apiVersion}/authenticate/generateToken", data);

                customeLoadingWindow.Close();

                this.IsEnabled = true;

                if (!response.Data.Result)
                {
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        CustomMessageBox interNalServerErrorMessage = new CustomMessageBox("خطا", "لطفا از اتصال خود با اینترنت اطمینان حاصل فرمایید", "باشه", "", false);
                        interNalServerErrorMessage.ShowDialog();
                        return;
                    }

                    CustomMessageBox customMessageBox = new CustomMessageBox("خطای کاربر", "لطفا در ارسال اطلاعات دقت فرمایید", "باشه", "", false);
                    customMessageBox.ShowDialog();

                    txtPassword.Password = "";
                }
                else
                {
                    CustomMessageBox customMessageBox = new CustomMessageBox("خوش آمد گویی", $"خوش آمدید {response.Data.Username}", "باشه", "", false);
                    customMessageBox.ShowDialog();

                    TokenManager.Token = response.Data.Token;

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();

                    this.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}
