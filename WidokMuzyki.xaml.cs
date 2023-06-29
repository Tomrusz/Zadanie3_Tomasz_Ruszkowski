using System.Windows;

namespace Zadanie3_Tomasz_Ruszkowski
{
    /// <summary>
    /// Logika interakcji dla klasy WidokOsoby.xaml
    /// </summary>
    public partial class WidokMuzyki : Window
    {
        public WidokMuzyki(Muzyka muzyka)
        {
            DataContext = muzyka;
            InitializeComponent();
        }

        private void OK(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
