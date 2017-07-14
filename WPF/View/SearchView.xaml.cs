using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for SearchView.xaml
    /// </summary>
    public partial class SearchView : Window
    {
        public SearchView()
        {
            InitializeComponent();

            Messenger.Default.Register<NotificationMessage>(this, nm =>
            {
                if (nm.Notification == "CloseSearch")
                {
                    Messenger.Default.Unregister<NotificationMessage>(this, "CloseSearch");
                    Close();
                }
            });
        }
    }
}
