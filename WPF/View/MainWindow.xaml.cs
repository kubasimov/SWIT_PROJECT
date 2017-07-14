using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseMain")
                {
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseMain");
                    Close();
                }
            });
        }

        
    }
}
