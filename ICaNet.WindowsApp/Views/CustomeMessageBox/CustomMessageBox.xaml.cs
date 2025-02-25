using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ICaNet.WindowsApp.Views.CustomeMessageBox
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox(string title, string message, string btnOkName, string btnNoName, bool isTwoButton)
        {
            InitializeComponent();
            lblTextMessage.Text = message;
            lblTitle.Text = title;
            btnNo.Content = btnNoName;
            btnOk.Content = btnOkName;
            if (!isTwoButton)
            {
                btnNo.Visibility = Visibility.Collapsed;
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            this.BeginAnimation(UIElement.OpacityProperty, fadeIn);
        }
    }
}
